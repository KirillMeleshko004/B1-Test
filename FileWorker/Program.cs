using FileWorker.UI;

namespace FileWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("File Worker");

            Interactor.StartInteraction().Wait();
        }

    }
}