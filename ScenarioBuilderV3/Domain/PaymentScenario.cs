using ScenarioBuilderV3.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV3.Domain
{
    [Scenario]
    public sealed class PaymentScenario : IAttributedScenario
    {
        [ScenarioStep(typeof(ChargePaymentStep))]
        public ChargePaymentEvent? ChargePayment { get; init; }
    }
}