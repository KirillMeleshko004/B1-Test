namespace FileWorker.Utility
{
    public class FileWriter
    {
        private string _path;
        private object _lock = new();

        public FileWriter(string filePath)
        {
            _path = filePath;
            File.Delete(_path);
        }

        public void InsertRows(IEnumerable<string> rows)
        {
            //Lock to limit single file usage to only one thread
            lock (_lock)
            {
                File.AppendAllLines(_path, rows);
            }
        }

    }
}