using OrderAggregator.Models;
using OrderAggregator.Services.OrderStorageService;

namespace OrderAggregator.Tests
{
    public class LocalOrderStorageServiceTests
    {
        [Test]
        public async Task AddOrders_ShouldAddOrdersToStorage()
        {
            // Arrange
            var orderStorageService = new LocalOrderStorageService();
            var orders = new List<OrderItem>
                {
                    new OrderItem { ProductId = "P1", Quantity = 5 },
                    new OrderItem { ProductId = "P2", Quantity = 10 },
                    new OrderItem { ProductId = "P3", Quantity = 3 }
                };

            // Act
            var result = await orderStorageService.AddOrdersAsync(orders);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(3, orderStorageService.GetOrdersAsync().Result.Count);
        }

        [Test]
        public async Task AddOrders_ShouldAddOrdersAsDict()
        {
            // Arrange
            var orderStorageService = new LocalOrderStorageService();
            var orders = new Dictionary<string,int>
                {
                    { "P1", 5 },
                    { "P2", 10 },
                    { "P3", 3 }
                };

            // Act
            var result = await orderStorageService.AddOrdersAsync(orders);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(3, orderStorageService.GetOrdersAsync().Result.Count);
        }

        [Test]
        public async Task GetOrders_ShouldReturnAllOrdersAndClearStorage()
        {
            // Arrange
            var orderStorageService = new LocalOrderStorageService();
            var orders = new Dictionary<string, int>
                {
                    { "P1", 5 },
                    { "P2", 10 },
                    { "P3", 3 }
                };
            await orderStorageService.AddOrdersAsync(orders);

            // Act
            var result = await orderStorageService.GetOrdersAsync();

            // Assert
            Assert.AreEqual(orders, result);
            Assert.IsEmpty(orderStorageService.GetOrdersAsync().Result);
        }

        [Test]
        public async Task UpdateOrders_ShouldUpdateValuesInStorage()
        {
            // Arrange
            var orderStorageService = new LocalOrderStorageService();
            var orders = new Dictionary<string, int>
            {
                { "P1", 5 }                
            };
            await orderStorageService.AddOrdersAsync(orders);

            // Act            
            var result = await orderStorageService.AddOrdersAsync(new Dictionary<string,int> { { "P1", 7 } });

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(12, orderStorageService.GetOrdersAsync().Result["P1"]);
        }
    }
}