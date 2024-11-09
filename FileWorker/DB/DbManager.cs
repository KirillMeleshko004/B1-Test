using System.Data;
using System.Globalization;
using FileWorker.UI.Interfaces;
using Microsoft.Data.SqlClient;

namespace FileWorker.DB
{
    public class DbManager
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

        private const string _masterConnectionString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";
        private const string _connectionString = @"Server=localhost\SQLEXPRESS;Database=Test;Trusted_Connection=True;TrustServerCertificate=True;";
        private const string _dbName = "RecordsDb";
        private const string _tableName = "Records";

        private readonly IInteraction _interaction;

        public DbManager(IInteraction interaction)
        {
            _interaction = interaction;
            EnsureDbCreated();
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

        public async Task<(int intSum, double floatMedian)> CalculateIntSumAndFloatMedian()
        {
            throw new NotImplementedException();
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

        private void EnsureDbCreated()
        {
            using (SqlConnection connection = new(_masterConnectionString))
            {
                if (!DoesDbExist(connection, _dbName))
                {
                    connection.Open();

                    SqlCommand createDbCommand = new()
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
                if (!DoesTableExist(connection, _tableName))
                {
                    connection.Open();
                    SqlCommand createTableCommand = new()
                    {
                        CommandText = $"CREATE TABLE {_tableName} (Date DATE NOT NULL, Latin NVARCHAR(10) NOT NULL, Cyrillic NVARCHAR(10) NOT NULL, PositiveEven INT NOT NULL, Float DECIMAL(38,8) NOT NULL)",
                        Connection = connection
                    };

                    createTableCommand.ExecuteNonQuery();

                    _interaction.ShowMessage($"Table {_tableName} was created.");
                }
            }
        }

        public static bool DoesDbExist(SqlConnection connection, string databaseName)
        {
            connection.Open();

            DataTable tableInfo = connection.GetSchema("DATABASES", [databaseName]);

            connection.Close();

            return tableInfo.Rows.Count > 0;
        }

        private static bool DoesTableExist(SqlConnection connection, string TableName)
        {
            connection.Open();

            //[null, null, TableName] - [TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME]
            DataTable tableInfo = connection.GetSchema("TABLES", [null, null, TableName]);

            connection.Close();

            return tableInfo.Rows.Count > 0;
        }

        #endregion
    }

}