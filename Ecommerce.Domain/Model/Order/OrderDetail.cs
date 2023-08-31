using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Model.Order
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderHeaderId { get; set; }
        [ForeignKey(nameof(OrderHeaderId))]
        public OrderHeader? OrderHeader { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product? Product;

        public int Count { get; set; }
        public double PricePaid { get; set; }
    }
}
