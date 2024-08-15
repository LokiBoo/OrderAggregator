using Newtonsoft.Json;

namespace OrderAggregator.Models.ExportProcessors
{
    public class ConsoleExportProcessor : IExportProcessor
    {
        private readonly ILogger<ConsoleExportProcessor> _logger;

        public ConsoleExportProcessor(ILogger<ConsoleExportProcessor> logger)
        {
            _logger = logger;
        }

        public async Task<bool> ExportAsync(Dictionary<string, int> orders)
        {
            Console.WriteLine(JsonConvert.SerializeObject(orders));
            _logger.LogInformation("Orders exported to console");
            return true;
        }
    }
}
