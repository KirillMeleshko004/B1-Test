using System.Diagnostics;
using System.Text.RegularExpressions;
using FileWorker.DB;
using FileWorker.UI.Interfaces;
using Microsoft.Data.SqlClient;

namespace FileWorker.Services
{
    public class FileImportService(IInteraction interaction, string filePath) : BaseService(interaction)
    {
        private string _filePath = filePath;


        private const string DATA_FILE_REGEX = @"([0-9]|[1-9][0-9]|100).txt$";

        public async Task ImportFiles()
        {
            _interaction.ShowMessage("File import starts.");

            if (!File.Exists(_filePath))
            {
                _interaction.ShowMessage("Selecter file doesn't exist.");
            }

            var dbManager = new DbManager(_interaction);

            await dbManager.ImportFile(_filePath);
        }

    }
}