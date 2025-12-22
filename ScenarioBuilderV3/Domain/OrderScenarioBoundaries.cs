using Microsoft.Extensions.DependencyInjection;
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
        private readonly ServiceProvider _provider;

        public OrderScenarioBoundaries(ServiceProvider provider)
        {
            _options = new ScenarioExecutionOptions();
            _provider = provider;
        }

        public ScenarioExecutionOptions ByCreatingTheOrder() => _options.RunUntil<CreateOrderEvent>();

        public ScenarioExecutionOptions ByReservingInventory() => _options.RunUntil<ReserveInventoryEvent>();

        public ScenarioExecutionOptions ByFailingPayment() => _options.Override<ChargePaymentEvent, ChargePaymentEventFail>(_provider);

        public ScenarioExecutionOptions BySettingTheShipping() => _options.RunUntil<ShipOrderEvent>();
    }
}