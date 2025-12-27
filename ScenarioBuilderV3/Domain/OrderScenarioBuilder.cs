using Microsoft.Extensions.DependencyInjection;
using ScenarioBuilderV3.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV3.Domain
{
    // Domain/OrderScenarioBoundaries.cs
    public sealed class OrderScenarioBuilder : IScenarioOptionsBuilder<OrderScenarioBuilder>
    {
        public ScenarioExecutionOptions ScenarioOptions { get; }

        private IServiceProvider provider;

        public ScenarioExecutionOptions Options => ScenarioOptions;

        public OrderScenarioBuilder(ScenarioExecutionOptions options, IServiceProvider p)
        {
            ScenarioOptions = options;
            this.provider = p;
        }

        public OrderScenarioBuilder ByCreatingTheOrder()
        {
            ScenarioOptions.RunUntil<CreateOrderEvent>();
            return this;
        }

        public OrderScenarioBuilder ByReservingInventory()
        {
            ScenarioOptions.RunUntil<ReserveInventoryEvent>();
            return this;
        }

        public OrderScenarioBuilder ByFailingPayment()
        {
            ScenarioOptions.Override<ChargePaymentEvent, ChargePaymentEventFail>(provider);
            return this;
        }

        public OrderScenarioBuilder BySettingTheShipping()
        {
            ScenarioOptions.RunUntil<ShipOrderEvent>();
            return this;
        }
    }
}