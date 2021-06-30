using System;

namespace Sulmar.EFCore.Models
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public string NIP { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public CustomerType CustomerType { get; set; }
        public byte[] Avatar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Address InvoiceAddress { get; set; }
        public Address ShipAddress { get; set; }
        public Coordinate Location { get; set; } // -> string
        public bool IsRemoved { get; set; }
    }

    public enum CustomerType
    {
        Private,
        Company
    }
}





