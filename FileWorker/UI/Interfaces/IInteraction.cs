namespace FileWorker.UI.Interfaces
{
    public interface IInteraction
    {
        string Title { get; }

        void ShowMessage(string message);
        string GetInput(string inputPrompt);
        char GetConfirmation(string confirmationPrompt);
        void Clear();
    }
}