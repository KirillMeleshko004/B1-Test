using FileWorker.UI.Interfaces;

namespace FileWorker.UI
{
    public class ConsoleInteraction : IInteraction, IDisposable
    {
        public string Title { get; private set; } = string.Empty;

        public ConsoleInteraction(string title)
        {
            Title = title;

            Clear();
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string GetInput(string inputPrompt)
        {
            Console.Write($"{inputPrompt}: ");
            var input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            return input;
        }

        public char GetConfirmation(string confirmationPrompt)
        {
            Console.Write($"{confirmationPrompt}");
            var input = Console.ReadKey();
            Console.WriteLine();

            return input.KeyChar;
        }

        public void Clear()
        {
            //Default VS Code debug fails why Console.Clear() called
            if (!Console.IsInputRedirected) Console.Clear();

            Console.WriteLine("".PadRight(20, '-'));
            Console.WriteLine(Title);
            Console.WriteLine("\n".PadLeft(20, '-'));
        }

        public void Dispose()
        {
            if (!Console.IsInputRedirected) Console.Clear();
            GC.SuppressFinalize(this);
        }
    }
}