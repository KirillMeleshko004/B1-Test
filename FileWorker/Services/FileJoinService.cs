namespace FileWorker.Services
{
    public class FileJoinService(string path)
    {
        private readonly string _path = path;
        private const string DEFAULT_UNION_PATH = "Union";

        private string[] _files = [];

        /// <summary>
        /// Join all files from provided directory into one and exclude all rows containing specified string
        /// </summary>
        /// <param name="exclude"></param>
        /// <returns>Number of excluded rows</returns>
        public async Task<int> JoinFiles(string fileName = "Union.txt", string? exclude = null)
        {
            var unionName = fileName;

            _files = Directory.GetFiles(_path);

            string outputPath = Path.Combine(_path, DEFAULT_UNION_PATH);

            DirectoryInfo outputDir = new(outputPath);
            outputDir?.Create();
            using var output = new StreamWriter(Path.Combine(outputPath, unionName), false);

            var toExclude = exclude?.Split(' ');

            if (toExclude == null || toExclude.Length == 0)
            {
                await JoinFiles(output);
                return 0;
            }
            else
            {
                return await JoinFiles(output, toExclude);
            }
        }

        private async Task JoinFiles(StreamWriter output)
        {
            foreach (var file in _files)
            {
                await foreach (var row in File.ReadLinesAsync(file))
                {
                    await output.WriteLineAsync(row);
                }
            }
        }

        private async Task<int> JoinFiles(StreamWriter output, string[] exclude)
        {
            int excluded = 0;
            foreach (var file in _files)
            {
                await foreach (var row in File.ReadLinesAsync(file))
                {
                    async Task ExclusionLoop()
                    {
                        foreach (var el in exclude)
                        {
                            if (row.Contains(el))
                            {
                                excluded++;
                                return;
                            }
                        }
                        await output.WriteLineAsync(row);
                    }

                    await ExclusionLoop();
                }
            }
            return excluded;
        }
    }
}