namespace Sulmar.EFCore.Models
{
    public class OrderDetail : BaseEntity
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineAmount => Quantity * UnitPrice;
    }
}





