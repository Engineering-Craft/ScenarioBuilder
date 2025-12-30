using ScenarioBuilder.Core.Attributes;
using ScenarioBuilder.Core.Interfaces;
using ScenarioBuilder.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Domain
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