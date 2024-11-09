using FileWorker.DB;
using FileWorker.UI.Interfaces;

namespace FileWorker.Services
{
    public class FileImportService(IInteraction interaction, string filePath) : BaseService(interaction)
    {
        private string _filePath = filePath;

        public async Task ImportFiles()
        {
            _interaction.ShowMessage("File import starts.");

            if (!File.Exists(_filePath))
            {
                _interaction.ShowMessage("Selecter file doesn't exist.");
            }

            var dbContext = await DbContext.CreateAsync(_interaction);

            await dbContext.ImportFile(_filePath);
        }

    }
}