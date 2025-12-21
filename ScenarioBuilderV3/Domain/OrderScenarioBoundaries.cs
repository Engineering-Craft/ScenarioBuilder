using ScenarioBuilderV3.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV3.Domain
{
    // Domain/OrderScenarioBoundaries.cs
    public sealed class OrderScenarioBoundaries
    {
        private readonly ScenarioExecutionOptions _options;

        private OrderScenarioBoundaries(ScenarioExecutionOptions options) => _options = options;

        public static OrderScenarioBoundaries Create() => new(new ScenarioExecutionOptions());

        public ScenarioExecutionOptions ByCreatingTheOrder() => _options.RunUntil<CreateOrderStep>();

        public ScenarioExecutionOptions ByReservingInventory() => _options.RunUntil<ReserveInventoryStep>();

        public ScenarioExecutionOptions ByChargingPayment() => _options.RunUntil<ChargePaymentStep>();

        public ScenarioExecutionOptions BySettingTheShipping() => _options.RunUntil<ShipOrderStep>();
    }
}