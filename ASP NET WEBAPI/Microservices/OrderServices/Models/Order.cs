
namespace OrderServices.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public String ProductName { get; set; } = string.Empty;

        public int Quantity { get; set; }
    }
}