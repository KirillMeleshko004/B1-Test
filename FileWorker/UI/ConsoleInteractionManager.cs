using System.Diagnostics;
using FileWorker.Services;
using FileWorker.UI.Interfaces;

namespace FileWorker.UI
{
    public class ConsoleInteractionManager : IInteractionManager
    {
        private readonly int[] _allowedOptions = [0, 1, 2, 3, 4];
        public async Task<bool> StartInteractionAsync()
        {
            try
            {
                int option;
                do
                {
                    option = MenuInteraction();

                    switch (option)
                    {
                        case 1:
                            {
                                await FileGenerationInteraction();
                                break;
                            }
                        case 2:
                            {
                                await FileJoinInteraction();
                                break;
                            }
                        case 3:
                            {
                                await FileImportInteraction();
                                break;
                            }
                        case 4:
                            {
                                await AggregationInteraction();
                                break;
                            }
                        case 0: break;
                    }
                }
                while (option != 0);

            }
            catch (Exception ex)
            {
                using var interaction = new ConsoleInteraction("Error");

                interaction.ShowMessage("Following error occured during execution:");
                interaction.ShowMessage(ex.Message);

                interaction.GetConfirmation("Press any key to continue...");

                return false;
            }

            return true;
        }

        private int MenuInteraction()
        {
            using var interaction = new ConsoleInteraction("Operation selection.");

            var isInputCorrect = false;
            var result = 0;

            do
            {
                interaction.ShowMessage("1. Generate files\n2. Join files\n3. Import to DB\n4. Sum and median\n0. Exit\n");

                var input = interaction.GetInput("Enter option");

                isInputCorrect = int.TryParse(input, out result);
                isInputCorrect = isInputCorrect && Array.Exists(_allowedOptions, x => x == result);

                if (!isInputCorrect)
                {
                    interaction.ShowMessage("Incorrect input.");
                    _ = interaction.GetConfirmation("Press any key to try again...");

                    interaction.Clear();
                }
            }
            while (!isInputCorrect);

            interaction.Clear();
            return result;
        }

        private async Task FileGenerationInteraction()
        {
            using var interaction = new ConsoleInteraction("File generation.");

            var dir = interaction.GetInput("Enter directory for files (empty for \"Data\" directory)");

            if (string.IsNullOrWhiteSpace(dir))
            {
                dir = "Data";
            }

            var fg = new FileGenerationService(interaction, dir);
            var sw = new Stopwatch();

            sw.Start();
            await fg.Generate(100, 100000);
            sw.Stop();

            interaction.ShowMessage($"File generation completed in {sw.ElapsedMilliseconds}ms.");
            interaction.GetConfirmation("Press any key to continue...");
            interaction.Clear();
        }

        private async Task FileJoinInteraction()
        {
            using var interaction = new ConsoleInteraction("File joining.");

            interaction.ShowMessage("Service join only files with same names, as generated ([number].txt).");

            var dir = interaction.GetInput("Enter directory with files (it should be directory containing generated files. Defailt - \"Data\")");

            if (string.IsNullOrWhiteSpace(dir))
            {
                dir = "Data";
            }

            var unionFile = interaction.GetInput($"Enter full path for union file (default is \"{dir}\\union.txt\")");

            if (string.IsNullOrWhiteSpace(unionFile))
            {
                unionFile = $"{dir}\\union.txt";
            }
            else if (!unionFile.EndsWith(".txt"))
            {
                unionFile += ".txt";
            }

            var shouldExclude = interaction.GetConfirmation("Exclude some sequences from union file? (y/N)\n");
            string? toExclude = null;

            if (char.ToLower(shouldExclude) == 'y')
            {
                toExclude = interaction.GetInput("Enter char sequences (separated by spaces) to exclude from union file");
            }

            var fj = new FileJoinService(interaction, dir, unionFile);
            var sw = new Stopwatch();

            sw.Start();
            await fj.JoinFiles(toExclude);
            sw.Stop();

            interaction.ShowMessage($"File joining completed in {sw.ElapsedMilliseconds}ms.");
            interaction.GetConfirmation("Press any key to continue...");
            interaction.Clear();
        }

        private async Task FileImportInteraction()
        {
            using var interaction = new ConsoleInteraction("File import.");

            var filePath = interaction.GetInput("Enter file path (Defailt - \"Data\\1.txt\")");

            if (string.IsNullOrWhiteSpace(filePath))
            {
                filePath = @"Data\1.txt";
            }

            var service = new FileImportService(interaction, filePath);
            var sw = new Stopwatch();

            sw.Start();
            await service.ImportFiles();
            sw.Stop();

            interaction.ShowMessage($"File import completed in {sw.ElapsedMilliseconds}ms.");
            interaction.GetConfirmation("Press any key to continue...");
            interaction.Clear();
        }

        private async Task AggregationInteraction()
        {
            using var interaction = new ConsoleInteraction("Sum and median calculation.");

            var service = new AggregationService(interaction);
            var sw = new Stopwatch();

            sw.Start();
            var (intSum, floatMedian) = await service.CalculateSumAndMedian();
            sw.Stop();

            interaction.ShowMessage($"Sum and median calculation completed in {sw.ElapsedMilliseconds}ms.");
            interaction.ShowMessage($"Integer numbers sum: {intSum}.");
            interaction.ShowMessage($"Float numbers median: {floatMedian}.");
            interaction.GetConfirmation("Press any key to continue...");
            interaction.Clear();
        }
    }
}