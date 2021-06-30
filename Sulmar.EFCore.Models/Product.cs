namespace Sulmar.EFCore.Models
{
    public class Product : Item
    {
        public string Color { get; set; }
        public float Weight { get; set; }
        public string BarCode { get; set; }
        public string Size { get; set; }
        public ProductParameters Parameters { get; set; } // -> string w formacie json
    }

    public class ProductParameters : Base
    {
        public float Power { get; set; }
        public int Capacity { get; set; }
        public string SerialNumber { get; set; }
    }
}





