using FileWorker.UI;

namespace FileWorker
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var manager = new ConsoleInteractionManager();

            var result = await manager.StartInteractionAsync();

            Console.WriteLine($"Proggram finished with code {(result ? 0 : 1)}");
        }

    }
}