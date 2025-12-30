using ScenarioBuilder.Core;
using ScenarioBuilder.Core.Attributes;
using ScenarioBuilder.Core.Interfaces;

namespace ScenarioBuilder.Domain
{
    [Scenario]
    public class OrderScenario : Scenario, IScenario
    {
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