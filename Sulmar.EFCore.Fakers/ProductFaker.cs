using Bogus;
using Sulmar.EFCore.Models;
using System.Collections.Generic;
using System.Text;

namespace Sulmar.EFCore.Fakers
{
    public class ProductFaker : Faker<Product>
    {
        public ProductFaker()
        {
            string[] sizes = new string[] { "S", "M", "L", "XL" };

            StrictMode(true);
            Ignore(p => p.Id);
            RuleFor(p => p.Name, f => f.Commerce.ProductName());
            RuleFor(p => p.BarCode, f => f.Commerce.Ean13());
            RuleFor(p => p.Color, f => f.Commerce.Color());
            RuleFor(p => p.CreatedOn, f => f.Date.Past());
            RuleFor(p => p.UnitPrice, f => decimal.Parse(f.Commerce.Price()));
            RuleFor(p => p.Weight, f => f.Random.Float(1, 1000));
            RuleFor(p => p.Size, f => f.PickRandom(sizes));
            Ignore(p => p.ModifiedOn);

            Ignore(p => p.Parameters);
        }
    }
}
