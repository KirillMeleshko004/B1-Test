using FileWorker.UI.Interfaces;
using FileWorker.Utility;

namespace FileWorker.Services
{
    public class FileGenerationService(IInteraction interaction, string path) : BaseService(interaction)
    {
        private readonly string _path = path;

        public async Task Generate(int filesCount, int rowsCount)
        {
            _interaction.ShowMessage("File generation starts.");

            DirectoryInfo dirInfo = new(_path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
                _interaction.ShowMessage($"Directory \"{dirInfo.FullName}\" was created.");
            }

            Task[] tasks = new Task[filesCount];

            for (int file = 0; file < filesCount; file++)
            {
                var ind = file;

                //Generating each file in separated task executed on ThreadPool
                tasks[file] = Task.Run(() =>
                {
                    var fileName = $"{ind + 1}.txt";
                    var fullPath = Path.Combine(_path, fileName);

                    var isReplaced = File.Exists(fullPath);

                    using (var writer = new StreamWriter(fullPath, false))
                    {
                        for (int row = 0; row < rowsCount; row++)
                        {
                            writer.WriteLine(DataGen.GenerateDataRow());
                        }
                    }

                    if (isReplaced)
                    {
                        _interaction.ShowMessage($"Replaced file \"{fileName}\" {Environment.CurrentManagedThreadId}.");
                    }
                    else
                    {
                        _interaction.ShowMessage($"Created file \"{fileName}\" {Environment.CurrentManagedThreadId}.");
                    }
                });
            }

            await Task.WhenAll(tasks);
        }
    }
}