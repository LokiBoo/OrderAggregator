using OrderAggregator.Models;

namespace OrderAggregator.Services.OrderStorageService
{
    /// <summary>
    /// Interface for service that stores orders
    /// </summary>
    public interface IOrderStorageService
    {
        /// <summary>
        /// Add orders to the storage
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        Task<bool> AddOrdersAsync(IDictionary<string, int> orders);

        /// <summary>
        /// Add orders to the storage
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddOrdersAsync(IEnumerable<OrderItem> request);

        /// <summary>
        /// Get all orders from the storage and clear it
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, int>> GetOrdersAsync();
    }
}