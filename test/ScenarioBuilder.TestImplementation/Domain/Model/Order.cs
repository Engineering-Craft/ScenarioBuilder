using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Tests.ScenarioBuilder.TestImplementation.Domain.Model
{
    public class Order
    {
        public object Id { get; internal set; }
        public object CompanyName { get; internal set; }
        public object ContactName { get; internal set; }
        public object Phone { get; internal set; }
    }
}