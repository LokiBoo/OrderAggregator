
namespace OrderAggregator.Models.ExportProcessors
{
    public interface IExportProcessor
    {
        Task<bool> ExportAsync(Dictionary<string, int> orders);
    }
}