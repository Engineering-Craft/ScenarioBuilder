using Microsoft.Extensions.DependencyInjection;
using ScenarioBuilder.Core.Interfaces;
using ScenarioBuilder.Core;
using ScenarioBuilder.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Domain
{
    // Domain/OrderScenarioBoundaries.cs
    public sealed class OrderScenarioBuilder : IScenarioOptionsBuilder
    {
        private ScenarioExecutionOptions ScenarioOptions { get; }

        public ScenarioExecutionOptions Options => ScenarioOptions;

        public OrderScenarioBuilder()
        {
            ScenarioOptions = new ScenarioExecutionOptions();
            Shipping = new Shipping();

            _services = new Lazy<IServiceProvider>(() =>
            {
                var services = new ServiceCollection();

                // Core services
                services.AddScoped<ScenarioContext>();

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
            });
        }

        private readonly Lazy<IServiceProvider> _services;

        private IServiceProvider Services => _services.Value;

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

        public OrderScenarioBuilder BySettingTheShipping(Shipping dto)
        {
            this.Shipping = dto;
            return this;
        }

        public Shipping Shipping { get; private set; }

        IServiceProvider IScenarioOptionsBuilder.Services => Services;
    }
}