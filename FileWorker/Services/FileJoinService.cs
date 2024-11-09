using System.Text.RegularExpressions;
using FileWorker.UI.Interfaces;
using FileWorker.Utility;

namespace FileWorker.Services
{
    public class FileJoinService(IInteraction interaction, string path, string unionFile) : BaseService(interaction)
    {
        private const string DATA_FILE_REGEX = @"([0-9]|[1-9][0-9]|100).txt$";

        private readonly string _path = path;
        private readonly string _unionFile = unionFile;
        private string[] _files = [];

        private readonly FileWriter _fw = new(unionFile);

        public async Task JoinFiles(string? exclude = null)
        {
            _interaction.ShowMessage("File joining starts.");

            _files = Directory.GetFiles(_path).Where(f => Regex.IsMatch(f, DATA_FILE_REGEX)).ToArray();

            if (_files.Length == 0)
            {
                _interaction.ShowMessage("Provided path contains no files with matching names.");
                return;
            }

            var toExclude = exclude?.Split(' ');

            if (toExclude == null || toExclude.Length == 0)
            {
                await JoinFiles();
            }
            else
            {
                var excluded = await JoinFiles(toExclude);
                _interaction.ShowMessage($"Excluded {excluded} rows from result.");
            }

            _interaction.ShowMessage($"Union file path {new FileInfo(_unionFile).FullName}.");
        }

        private async Task JoinFiles()
        {
            var tasks = new List<Task>(_files.Length);

            foreach (var file in _files)
            {
                tasks.Add(Task.Run(async () =>
                {
                    _interaction.ShowMessage($"Proceeding file \"{file}\".");
                    var lines = new List<string>(100000);
                    await foreach (var row in File.ReadLinesAsync(file))
                    {
                        lines.Add(row);
                    }

                    _fw.InsertRows(lines);
                }));
            }

            await Task.WhenAll(tasks);

        }

        private async Task<int> JoinFiles(string[] toExclude)
        {
            int excluded = 0;
            var tasks = new List<Task>(_files.Length);

            foreach (var file in _files)
            {
                tasks.Add(Task.Run(async () =>
                {
                    _interaction.ShowMessage($"Proceeding file \"{file}\".");
                    var linesToAdd = new List<string>(100000);
                    var localExcluded = 0;

                    await foreach (var row in File.ReadLinesAsync(file))
                    {
                        if (toExclude.Any(ex => row.Contains(ex)))
                        {
                            localExcluded++;
                            continue;
                        }
                        linesToAdd.Add(row);
                    }

                    _fw.InsertRows(linesToAdd);
                    Interlocked.Add(ref excluded, localExcluded);
                }));
            }

            await Task.WhenAll(tasks);

            return excluded;
        }
    }
}