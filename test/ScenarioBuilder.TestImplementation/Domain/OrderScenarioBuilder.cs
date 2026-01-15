using Microsoft.Extensions.DependencyInjection;
using ScenarioBuilder.Core.Interfaces;
using ScenarioBuilder.Core;
using ScenarioBuilder.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using ScenarioBuilder.Tests.ScenarioBuilder.TestImplementation.Domain.Model;
using ScenarioBuilder.Tests.ScenarioBuilder.TestImplementation.Domain.Model.Fakers;

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

            Order = new OrderFaker().Generate();
            Shipping = new ShippingFaker().Generate();

            _services = new Lazy<IServiceProvider>(() =>
            {
                var services = new ServiceCollection();

                // Core services
                services.AddScoped<ScenarioBuilderContext>();

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
            ScenarioOptions.RunUntil<ChargePaymentEvent>();
            ScenarioOptions.Override<ChargePaymentEvent, ChargePaymentEventFail>(Services);
            return this;
        }

        public OrderScenarioBuilder BySettingTheShipping(Shipping dto)
        {
            this.Shipping = dto;
            return this;
        }

        public OrderScenarioBuilder BySettingTheOrder(Order dto)
        {
            this.Order = dto;
            return this;
        }

        public OrderScenarioBuilder BySettingTheOrderType(string type)
        {
            this.Order.Type = type;
            return this;
        }

        public Shipping Shipping { get; private set; }
        public Order Order { get; private set; }

        IServiceProvider IScenarioOptionsBuilder.Services => Services;
    }
}