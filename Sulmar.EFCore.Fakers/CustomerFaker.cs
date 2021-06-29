using Bogus;
using Sulmar.EFCore.Models;
using System;
using Bogus.Extensions.Poland;

namespace Sulmar.EFCore.Fakers
{

    // dotnet add package Bogus
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker()
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
        }
    }
}
