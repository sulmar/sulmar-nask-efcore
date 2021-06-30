using Bogus;
using Sulmar.EFCore.Models;
using System;
using Bogus.Extensions.Poland;

namespace Sulmar.EFCore.Fakers
{

    // dotnet add package Bogus
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker(Faker<Coordinate> coordinateFaker, Faker<Address> addressFaker)
        {
            StrictMode(true);

            // RuleFor(p => p.Id, f => f.IndexFaker);

            Ignore(p => p.Id);

            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.DateOfBirth, f => f.Person.DateOfBirth);
            RuleFor(p => p.CustomerType, f => f.PickRandom<CustomerType>());
            RuleFor(p => p.CreatedOn, f => f.Date.Past(2));            
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f));

            Ignore(p => p.Avatar);
            Ignore(p => p.ModifiedOn);
            Ignore(p => p.NIP);

            // dotnet add package Sulmar.Bogus.Extensions.Poland
            RuleFor(p => p.Pesel, f => f.Person.Pesel());

            RuleFor(p => p.InvoiceAddress, f => addressFaker.Generate());
            RuleFor(p => p.ShipAddress, f => addressFaker.Generate());
            RuleFor(p => p.Location, f => coordinateFaker.Generate());


            RuleFor(p => p.Amount, f => Math.Round(f.Random.Decimal(0, 1000), 0));
            Ignore(p => p.Version);
        }
    }

    public class AddressFaker : Faker<Address>
    {
        public AddressFaker()
        {
            RuleFor(p => p.City, f => f.Address.City());
            RuleFor(p => p.Country, f => f.Address.Country());
            RuleFor(p => p.Street, f => f.Address.StreetAddress());
            RuleFor(p => p.ZipCode, f => f.Address.ZipCode());
        }
    }

    public class CoordinateFaker : Faker<Coordinate>
    {
        public CoordinateFaker()
        {
            RuleFor(p => p.Latitude, f => f.Address.Latitude());
            RuleFor(p => p.Longitude, f => f.Address.Longitude());
        }
    }
}
