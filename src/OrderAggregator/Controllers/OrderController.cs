using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderAggregator.Models;
using OrderAggregator.Services.OrderStorageService;

namespace OrderAggregator.Controllers
{
    /// <summary>
    /// Controller for managing orders
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController: Controller
    {
        private readonly IOrderStorageService _orderStorageService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="orderStorageService"></param>
        public OrderController(IOrderStorageService orderStorageService)
        {
            _orderStorageService = orderStorageService;
        }

        /// <summary>
        /// Add orders to the storage
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddOrder(IEnumerable<OrderItem> request)
        {            
            if (await _orderStorageService.AddOrdersAsync(request))
            {
                return Ok();
            }
            
            return BadRequest();
        }        
    }
}
