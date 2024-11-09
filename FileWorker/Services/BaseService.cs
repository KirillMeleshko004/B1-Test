using FileWorker.UI.Interfaces;

namespace FileWorker.Services
{
    public abstract class BaseService
    {
        protected IInteraction _interaction;

        public BaseService(IInteraction interaction)
        {
            _interaction = interaction;
        }
    }
}