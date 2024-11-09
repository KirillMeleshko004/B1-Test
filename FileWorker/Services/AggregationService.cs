using FileWorker.DB;
using FileWorker.UI.Interfaces;

namespace FileWorker.Services

{
    public class AggregationService(IInteraction interaction) : BaseService(interaction)
    {
        public async Task<(long intSum, double floatMedian)> CalculateSumAndMedian()
        {
            _interaction.ShowMessage("Sum and median calculation starts.");

            var dbContext = await DbContext.CreateAsync(_interaction);

            return await dbContext.CalculateIntSumAndFloatMedian();
        }
    }
}
