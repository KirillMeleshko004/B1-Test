using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace FileWorker.Utility
{
    public class TextFileDataReader : IDataReader
    {
        private readonly StreamReader _sr;

        private readonly Func<string, object>[] _convertTable;
        private readonly Func<string, bool>[] _constraintTable;

        string[]? _currentLineValues;
        string? _currentLine;

        public TextFileDataReader(string filePath, Func<string, bool>[] constraintTable, Func<string, object>[] convertTable)
        {
            _constraintTable = constraintTable;
            _convertTable = convertTable;

            _sr = new StreamReader(filePath);

            _currentLine = null;
            _currentLineValues = null;
        }

        public object GetValue(int i)
        {
            try
            {
                return _convertTable[i](_currentLineValues![i]);
            }
            catch (Exception)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }

        public bool Read()
        {
            if (_sr.EndOfStream)
            {
                return false;
            }

            _currentLine = _sr.ReadLine();

            _currentLineValues = _currentLine!.Split("||").Take(5).ToArray();

            var invalidRow = false;

            for (int i = 0; i < _currentLineValues.Length; i++)
            {
                if (!_constraintTable[i](_currentLineValues[i]))
                {
                    invalidRow = true;
                    break;
                }
            }

            //If row is invalid, return Read() result from next line
            return !invalidRow || Read();
        }

        public int FieldCount
        {
            get { return 5; }
        }

        public void Dispose()
        {
            _sr.Close();
        }

        //Not implementing bunch of methods since those class has only one purpose in program –
        //to be used in SqlBulkCopy
        #region Non-implemented since not used

        public object this[int i] => throw new NotImplementedException();

        public object this[string name] => throw new NotImplementedException();

        public int Depth => throw new NotImplementedException();

        public bool IsClosed => throw new NotImplementedException();

        public int RecordsAffected => throw new NotImplementedException();


        public void Close()
        {
            throw new NotImplementedException();
        }


        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[]? buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[]? buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        [return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)]
        public Type GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public string GetName(int i)
        {
            throw new NotImplementedException();
        }

        public int GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        public DataTable? GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}