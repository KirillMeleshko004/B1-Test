using FileWorker.Services;

namespace FileWorker.UI
{
    public class Interactor
    {
        private static string? _dir;

        public static async Task StartInteraction()
        {
            Console.Write("Enter directory for files (empty for \"Data\" directory): ");
            _dir = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(_dir))
            {
                _dir = "Data";
            }

            await FilesGenInteraction();
            await MenuInteraction();
            // await FilesJoinInteraction();
        }

        private static async Task FilesGenInteraction()
        {
            Console.Write("Generate files? (y/n): ");
            var shouldCreate = char.ToLower(Console.ReadLine()?[0] ?? 'n') == 'y';
            Console.WriteLine();

            if (shouldCreate)
            {
                Console.WriteLine("Generating files...");
                var fg = new FileGenerationService(_dir!);
                await fg.Generate(100, 100000);
                Console.WriteLine("Generated 100 files");
            }
        }

        private static async Task MenuInteraction()
        {
            int operation;

            do
            {
                ShowMenu();
                Console.Write("Enter action: ");
                var input = Console.ReadLine();

                if (!int.TryParse(input, out operation))
                {
                    Console.WriteLine("Invalid input. Try again.");
                    continue;
                }

                switch (operation)
                {
                    case 1:
                        {
                            await FilesJoinInteraction();
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("DB import");
                            break;
                        }
                    case 3: return;
                }
            }
            while (operation > 0);

        }

        private static void ShowMenu()
        {
            Console.WriteLine("\n".PadRight(20, '-'));
            Console.WriteLine("\nOperations menu\n");
            Console.WriteLine("1. Join files\n2. Import to DB\n0. Exit\n");
        }

        private static async Task FilesJoinInteraction()
        {
            Console.WriteLine("\n".PadRight(20, '-'));
            Console.WriteLine("\nJoining files\n");

            Console.Write("Enter file name union file (empty for \"Union\"): ");
            var unionName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(unionName))
            {
                unionName = "Union";
            }

            unionName += ".txt";

            Console.WriteLine("Enter char sequences separated by spaces to exclude from union file: ");
            var exclude = Console.ReadLine();

            var joinService = new FileJoinService(_dir!);
            Console.WriteLine("Joining files...");
            var excluded = await joinService.JoinFiles(unionName, exclude);

            System.Console.WriteLine($"Done. Excluded {excluded} rows from result.");

        }
    }
}