using OrderAggregator.Models.ExportProcessors;
using OrderAggregator.Services.OrderStorageService;

namespace OrderAggregator.Services
{
    /// <summary>
    /// Service for exporting orders
    /// Once in 20 seconds, it exports orders from the storage with registered export processor
    /// </summary>
    public class OrdersExportService : IOrdersExportService
    {
        private readonly IOrderStorageService _orderStorageService;
        private readonly IExportProcessor _exportProcessor;
        private readonly ILogger<OrdersExportService> _logger;
        private readonly Timer _timer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="orderStorageService"></param>
        /// <param name="exportProcessor"></param>
        /// <param name="logger"></param>
        public OrdersExportService(IOrderStorageService orderStorageService, IExportProcessor exportProcessor, ILogger<OrdersExportService> logger)
        {
            _timer = new Timer(async(s) => await ExportOrders(s), null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
            _orderStorageService = orderStorageService;
            _exportProcessor = exportProcessor;
            _logger = logger;
        }
                
        private async Task ExportOrders(object? state)
        {
            Dictionary<string, int>? ordersForExport = null;            

            try
            {
                ordersForExport = await _orderStorageService.GetOrdersAsync();
                if (ordersForExport is null || ordersForExport.Count == 0)
                {
                    // No orders to export
                    return;
                }

                if (!await _exportProcessor.ExportAsync(ordersForExport))
                {
                    await _orderStorageService.AddOrdersAsync(ordersForExport);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while exporting orders");
                // Return orders back to the storage
                if(ordersForExport is not null)
                {
                    await _orderStorageService.AddOrdersAsync(ordersForExport);
                }
            }
        }
    }
}
