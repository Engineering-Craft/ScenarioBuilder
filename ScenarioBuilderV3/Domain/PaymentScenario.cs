using ScenarioBuilderV3.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV3.Domain
{
    [Scenario]
    public sealed class PaymentScenario : IScenario
    {
        [ScenarioStep(typeof(ChargePaymentEvent))]
        public ChargePaymentEvent? ChargePayment { get; init; }

        [ScenarioStep(typeof(VerifyPaymentEvent))]
        public VerifyPaymentEvent? VerifyPayment { get; init; }

        [NestedScenario(typeof(AnotherScenario))]
        public AnotherScenario? PaymentScenario1 { get; init; }
    }
}