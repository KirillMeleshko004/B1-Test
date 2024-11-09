using System.Data;
using System.Globalization;
using FileWorker.UI.Interfaces;
using FileWorker.Utility;
using Microsoft.Data.SqlClient;

namespace FileWorker.DB
{
    public class DbContext
    {
        private readonly Func<string, bool>[] _columnConstraints =
        [
            date => DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _),
            latin => !string.IsNullOrEmpty(latin),
            cyrillic => !string.IsNullOrEmpty(cyrillic),
            positiveEven => int.TryParse(positiveEven, out _),
            floatNumber => double.TryParse(floatNumber, out _),
        ];

        private readonly Func<string, object>[] _columnConverts =
        [
            date => DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture),
            latin => latin,
            cyrillic => cyrillic,
            positiveEven => int.Parse(positiveEven),
            floatNumber => double.Parse(floatNumber)
        ];

        private const string _dbName = "RecordsDb";
        private const string _tableName = "Records";
        private const string _masterConnectionString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";
        private const string _connectionString = $@"Server=localhost\SQLEXPRESS;Database={_dbName};Trusted_Connection=True;TrustServerCertificate=True;";
        private const string _intSumSp = "usp_SumIntegers";
        private const string _floatMedianSp = "usp_FloatMedian";

        private readonly IInteraction _interaction;

        private DbContext(IInteraction interaction)
        {
            _interaction = interaction;
        }

        public static async Task<DbContext> CreateAsync(IInteraction interaction)
        {
            var dbContext = new DbContext(interaction);

            await dbContext.EnsureDbCreated();
            await dbContext.EnsureProceduresCreated();

            return dbContext;
        }

        public async Task ImportFile(string filePath)
        {
            var rowsCount = await CountFileLines(filePath);

            var dataReader = new TextFileDataReader(filePath, _columnConstraints, _columnConverts);

            using var bulkCopy = new SqlBulkCopy(_connectionString, SqlBulkCopyOptions.Default);

            bulkCopy.ColumnMappings.Add(0, 0);
            bulkCopy.ColumnMappings.Add(1, 1);
            bulkCopy.ColumnMappings.Add(2, 2);
            bulkCopy.ColumnMappings.Add(3, 3);
            bulkCopy.ColumnMappings.Add(4, 4);

            bulkCopy.DestinationTableName = _tableName;
            bulkCopy.BatchSize = 5000;
            bulkCopy.NotifyAfter = 5000;

            bulkCopy.SqlRowsCopied += (sender, e) =>
            {
                _interaction.ShowMessage($"Proceeded {e.RowsCopied} of {rowsCount}. Left: {rowsCount - e.RowsCopied}.");
            };

            await bulkCopy.WriteToServerAsync(dataReader);
        }

        public async Task<(long intSum, double floatMedian)> CalculateIntSumAndFloatMedian()
        {
            (long intSum, double floatMedian) result = (0, 0);

            using (SqlConnection connection = new(_connectionString))
            {
                result.intSum = await GetIntegerSum(connection);
                result.floatMedian = await GetFloatMedian(connection);
            }

            return result;
        }

        #region Private methods

        private static async Task<int> CountFileLines(string filePath)
        {
            var lineCount = 0;

            using var reader = File.OpenText(filePath);
            while (await reader.ReadLineAsync() != null)
            {
                lineCount++;
            }

            return lineCount;
        }

        private async Task<long> GetIntegerSum(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
            {
                await connection.OpenAsync();
            }

            using SqlCommand sumCommand = new(_intSumSp, connection);
            sumCommand.CommandType = CommandType.StoredProcedure;
            sumCommand.Parameters.Add(new()
            {
                ParameterName = "@Sum",
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.Output
            });

            await sumCommand.ExecuteNonQueryAsync();

            await connection.CloseAsync();
            return long.Parse(sumCommand.Parameters["@Sum"].Value.ToString()!);
        }

        private async Task<double> GetFloatMedian(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
            {
                await connection.OpenAsync();
            }

            using SqlCommand medianCommand = new(_floatMedianSp, connection);
            medianCommand.CommandType = CommandType.StoredProcedure;
            medianCommand.Parameters.Add(new()
            {
                ParameterName = "@Median",
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Output
            });

            medianCommand.Parameters["@Median"].Precision = 38;
            medianCommand.Parameters["@Median"].Scale = 8;

            await medianCommand.ExecuteNonQueryAsync();

            await connection.CloseAsync();
            return double.Parse(medianCommand.Parameters["@Median"].Value.ToString()!);
        }

        private async Task EnsureDbCreated()
        {
            using (SqlConnection connection = new(_masterConnectionString))
            {
                if (!await DoesDbExist(connection, _dbName))
                {
                    await connection.OpenAsync();

                    using SqlCommand createDbCommand = new()
                    {
                        CommandText = $"CREATE DATABASE {_dbName}",
                        Connection = connection
                    };

                    createDbCommand.ExecuteNonQuery();

                    _interaction.ShowMessage($"Database {_dbName} was created.");
                }
            }

            using (SqlConnection connection = new(_connectionString))
            {
                if (!await DoesTableExist(connection, _tableName))
                {
                    connection.Open();
                    using SqlCommand createTableCommand = new()
                    {
                        CommandText =
                            $@"CREATE TABLE {_tableName} 
                            (
                                Date DATE NOT NULL, 
                                Latin NVARCHAR(10) NOT NULL, 
                                Cyrillic NVARCHAR(10) NOT NULL, 
                                PositiveEven BIGINT NOT NULL, 
                                Float DECIMAL(38,8) NOT NULL
                            )",
                        Connection = connection
                    };

                    createTableCommand.ExecuteNonQuery();

                    _interaction.ShowMessage($"Table {_tableName} was created.");
                }
            }
        }

        private async Task EnsureProceduresCreated()
        {
            string sumProcedureCreation =
                $@"CREATE PROCEDURE {_intSumSp} 
                    @Sum BIGINT OUTPUT 
                AS 
                BEGIN
                    SELECT @Sum=SUM(PositiveEven) 
                    FROM {_tableName}
                END;";

            string floatMedianCreation =
                $@"CREATE PROCEDURE {_floatMedianSp} 
                    @Median DECIMAL(38,8) OUTPUT 
                AS 
                BEGIN
                    DECLARE @count BIGINT = (SELECT COUNT(*) FROM {_tableName});
                    SELECT @Median=AVG(Middle.Val)
                    FROM (
                        SELECT Float as Val FROM {_tableName}
                        ORDER BY Float
                        OFFSET (@count - 1) / 2 ROWS
                        FETCH NEXT 1 + (1 - @count % 2) ROWS ONLY
                    ) AS Middle
                END;";

            using SqlConnection connection = new(_connectionString);

            if (!await DoesProcedureExist(connection, _intSumSp))
            {
                await connection.OpenAsync();

                using SqlCommand command = new(sumProcedureCreation, connection);

                await command.ExecuteNonQueryAsync();

                _interaction.ShowMessage($"Stored procedure \"{_intSumSp}\" was created.");
            }

            if (!await DoesProcedureExist(connection, _floatMedianSp))
            {
                await connection.OpenAsync();

                using SqlCommand command = new(floatMedianCreation, connection);

                await command.ExecuteNonQueryAsync();

                _interaction.ShowMessage($"Stored procedure \"{_floatMedianSp}\" was created.");
            }
        }

        private static async Task<bool> DoesDbExist(SqlConnection connection, string databaseName)
        {
            var queryDb =
                $@"SELECT TOP 1 1
                FROM sys.databases
                WHERE name = '{databaseName}'";
            var exist = false;

            if (connection.State == ConnectionState.Closed)
            {
                await connection.OpenAsync();
            }

            using (SqlCommand command = new(queryDb, connection))
            {
                using SqlDataReader reader = await command.ExecuteReaderAsync();

                exist = reader.Read();
            }

            await connection.CloseAsync();
            return exist;
        }

        private static async Task<bool> DoesTableExist(SqlConnection connection, string tableName)
        {
            var queryTable =
                $@"SELECT TOP 1 1
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_NAME = '{tableName}'";
            var exist = false;

            if (connection.State == ConnectionState.Closed)
            {
                await connection.OpenAsync();
            }

            using (SqlCommand command = new(queryTable, connection))
            {
                using SqlDataReader reader = await command.ExecuteReaderAsync();

                exist = reader.Read();
            }

            await connection.CloseAsync();
            return exist;
        }

        private static async Task<bool> DoesProcedureExist(SqlConnection connection, string procName)
        {
            var queryStoredProcedure =
                $@"SELECT TOP 1 1
                FROM INFORMATION_SCHEMA.ROUTINES
                WHERE ROUTINE_NAME = '{procName}'";
            var exist = false;

            if (connection.State == ConnectionState.Closed)
            {
                await connection.OpenAsync();
            }

            using (SqlCommand command = new(queryStoredProcedure, connection))
            {
                using SqlDataReader reader = await command.ExecuteReaderAsync();

                exist = reader.Read();
            }

            await connection.CloseAsync();
            return exist;
        }

        #endregion

    }

}