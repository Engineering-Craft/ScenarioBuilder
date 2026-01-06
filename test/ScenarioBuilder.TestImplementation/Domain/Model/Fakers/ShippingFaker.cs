using Bogus;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Tests.ScenarioBuilder.TestImplementation.Domain.Model.Fakers
{
    public class ShippingFaker : Faker<Shipping>
    {
        public ShippingFaker()
        {
            UseSeed(1) // Use any number
            .RuleFor(c => c.Method, f => "Air")
            .RuleFor(c => c.Name, f => "UPS");
        }
    }
}