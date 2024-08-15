using OrderAggregator.Models;
using System.Collections.Concurrent;

namespace OrderAggregator.Services.OrderStorageService
{
    /// <summary>
    /// Implementation of IOrderStorageService that stores orders in memory
    /// </summary>
    /// <inheritdoc />
    public class LocalOrderStorageService : IOrderStorageService
    {
        // Thread safe collection.
        // Acorrding to the task, number of ProductId is relatively small, so we can use ConcurrentDictionary.
        private readonly ConcurrentDictionary<string, int> _aggregatedOrders; 
        private object _exportLock = new object();
              
        /// <summary>
        /// Constructor
        /// </summary>
        public LocalOrderStorageService()
        {
            _aggregatedOrders = new ConcurrentDictionary<string, int>();
        }
                
        public async Task<bool> AddOrdersAsync(IEnumerable<OrderItem> request)
        {
            foreach (var orderItem in request)
            {
                _aggregatedOrders.AddOrUpdate(orderItem.ProductId, orderItem.Quantity, (key, value) => value + orderItem.Quantity);
            }

            return true;
        }
                
        public async Task<bool> AddOrdersAsync(IDictionary<string, int> orders)
        {
            foreach (var orderItem in orders)
            {
                _aggregatedOrders.AddOrUpdate(orderItem.Key, orderItem.Value, (key, value) => value + orderItem.Value);
            }

            return true;
        }
                
        public async Task<Dictionary<string, int>> GetOrdersAsync()
        {
            var temp = new Dictionary<string, int>();
            lock (_exportLock)
            {
                temp = _aggregatedOrders.ToDictionary();
                _aggregatedOrders.Clear();
            }

            return temp;
        }
    }
}
