using Microsoft.Extensions.DependencyInjection;
using ScenarioBuilder.Core.Interfaces;
using ScenarioBuilder.Core;
using ScenarioBuilderV3.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV3.Domain
{
    // Domain/OrderScenarioBoundaries.cs
    public sealed class OrderScenarioBuilder : IScenarioOptionsBuilder
    {
        public ScenarioExecutionOptions ScenarioOptions { get; }

        public ScenarioExecutionOptions Options => ScenarioOptions;

        public OrderScenarioBuilder()
        {
            ScenarioOptions = new ScenarioExecutionOptions();
        }

        public IServiceProvider Services
        {
            get
            {
                var services = new ServiceCollection();

                // Core services
                services.AddScoped<ScenarioContext>();

                services.AddScoped<Scenario>();

                // Events
                services.AddAllScenarioEvents(typeof(OrderScenario).Assembly);

                services.AddTransient<IPaymentService, PaymentService>();

                // Scenario
                services.AddScoped<OrderScenario>();
                services.AddScoped<PaymentScenario>();

                //Builders
                services.AddScoped<OrderScenarioBuilder>();
                services.AddTransient<OrderScenarioBuilder>();

                services.AddTransient<ScenarioExecutionOptions>();
                services.AddTransient<OrderScenarioBuilder>();

                return services.BuildServiceProvider();
            }
        }

        public OrderScenarioBuilder ByReservingInventory()
        {
            ScenarioOptions.RunUntil<ReserveInventoryEvent>();
            return this;
        }

        public OrderScenarioBuilder ByFailingPayment()
        {
            ScenarioOptions.Override<ChargePaymentEvent, ChargePaymentEventFail>(Services);
            ScenarioOptions.RunUntil<ChargePaymentEvent>();
            return this;
        }

        public OrderScenarioBuilder BySettingTheShipping()
        {
            ScenarioOptions.RunUntil<ShipOrderEvent>();
            return this;
        }
    }
}