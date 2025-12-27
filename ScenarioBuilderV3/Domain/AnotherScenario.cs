using ScenarioBuilderV3.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV3.Domain
{
    [Scenario]
    public sealed class AnotherScenario : IScenario
    {
        //[ScenarioStep(typeof(ChargePaymentStep))]
        //public ChargePaymentEvent? ChargePayment { get; init; }

        //[ScenarioStep(typeof(VerifyPaymentStep))]
        //public VerifyPaymentEvent? VerifyPayment { get; init; }
        public IBuilder GetBuilder()
        {
            throw new NotImplementedException();
        }

        public IServiceProvider GetProvider()
        {
            throw new NotImplementedException();
        }
    }
}