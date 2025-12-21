using ScenarioBuilderV3.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV3.Domain
{
    [Scenario]
    public sealed class OrderFulfillmentScenario : IAttributedScenario
    {
        // Embed PaymentScenario as a nested scenario

        [ScenarioStep(typeof(CreateOrderEvent))]
        public CreateOrderEvent? CreateOrder { get; init; }

        [ScenarioStep(typeof(ReserveInventoryEvent))]
        public ReserveInventoryEvent? ReserveInventory { get; init; }

        [NestedScenario(typeof(PaymentScenario))]
        public PaymentScenario? PaymentScenario { get; init; }

        [ScenarioStep(typeof(ShipOrderEvent))]
        public ShipOrderEvent? ShipOrder { get; init; }
    }
}