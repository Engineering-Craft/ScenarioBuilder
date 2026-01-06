using Bogus;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Tests.ScenarioBuilder.TestImplementation.Domain.Model.Fakers
{
    public class OrderFaker : Faker<Order>
    {
        public OrderFaker()
        {
            UseSeed(1) // Use any number
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.CompanyName, f => f.Company.CompanyName())
            .RuleFor(c => c.ContactName, f => f.Name.FullName())
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumberFormat());
        }
    }
}