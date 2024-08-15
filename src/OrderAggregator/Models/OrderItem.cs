using System.ComponentModel.DataAnnotations;

namespace OrderAggregator.Models
{
    public class OrderItem
    {
        [StringLength(64, MinimumLength = 1)]
        public string ProductId { get; set; }
                
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
