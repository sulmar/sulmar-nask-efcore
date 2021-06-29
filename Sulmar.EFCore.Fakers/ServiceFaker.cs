using Bogus;
using Sulmar.EFCore.Models;
using System;

namespace Sulmar.EFCore.Fakers
{
    public class ServiceFaker : Faker<Service>
    {
        public ServiceFaker()
        {
            StrictMode(true);
            Ignore(p => p.Id);
            RuleFor(p => p.Name, f => f.Hacker.Verb());
            RuleFor(p => p.UnitPrice, f => decimal.Parse(f.Commerce.Price()));

            RuleFor(p => p.Duration, f => f.Date.Timespan(TimeSpan.FromHours(3)));

            RuleFor(p => p.CreatedOn, f => f.Date.Past());

            Ignore(p => p.ModifiedOn);
        }
    }
}
