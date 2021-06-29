namespace Sulmar.EFCore.Models
{
    public abstract class Item : BaseEntity
    {
        

        public string Name { get; set; }


        private decimal unitPrice;
        public decimal UnitPrice
        {
            get => unitPrice; 
            set
            {
                if (unitPrice != value)
                {
                    unitPrice = value;

                    OnPropertyChanged();
                }
            }
        }
    }
}





