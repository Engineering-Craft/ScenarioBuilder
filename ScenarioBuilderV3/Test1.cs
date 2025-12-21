using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScenarioBuilderV3.Core;
using ScenarioBuilderV3.Domain;
using System;
using System.Threading.Tasks;

namespace ScenarioEngine.Tests
{
    [TestClass]
    public class OrderFulfillmentScenarioTests
    {
        private ServiceProvider _provider = null!;

        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();

            // Core services
            services.AddScoped<ScenarioContext>();
            services.AddScoped<IScenarioBuilder, ScenarioBuilder>();
            services.AddScoped<Scenario>();

            // Events
            services.AddAllScenarioEvents(typeof(OrderFulfillmentScenario).Assembly);

            services.AddTransient<IPaymentService, PaymentService>();

            //    services.AddTransient<ScenarioEvent<PaymentScenario>>();

            // Scenario
            services.AddScoped<OrderFulfillmentScenario>();
            services.AddScoped<PaymentScenario>();

            _provider = services.BuildServiceProvider();
        }

        [TestMethod]
        public async Task RunScenarioUpToShippingStep()
        {
            // Arrange
            var executor = _provider.GetRequiredService<Scenario>();

            // Act: Run scenario up to ShipOrderStep using DSL
            var context = await executor.BuildAsync<OrderFulfillmentScenario>(
                OrderScenarioBoundaries
                    .Create()
                    .BySettingTheShipping() // stops at ShipOrderStep
            );

            // Assert: Check context has OrderId
            Assert.IsTrue(context.TryGet<Guid>("OrderId", out var orderId));
            Assert.AreNotEqual(Guid.Empty, orderId);

            // Optional: print for demo
            Console.WriteLine($"Order ID in scenario: {orderId}");
        }

        [TestMethod]
        public async Task RunScenarioThroughPaymentStep()
        {
            // Arrange
            var executor = _provider.GetRequiredService<Scenario>();

            // Act: Run scenario up to (but not including) ShipOrderStep
            var context = await executor.BuildAsync<OrderFulfillmentScenario>(
                OrderScenarioBoundaries
                    .Create()
                    .ByChargingPayment()
            );

            // Assert: OrderId exists
            Assert.IsTrue(context.TryGet<Guid>("OrderId", out var orderId));
            Assert.AreNotEqual(Guid.Empty, orderId);

            // Optional: could check side effects if events updated context
            Console.WriteLine($"Order ran through payment: {orderId}");
        }

        [TestMethod]
        public async Task RunFullScenario()
        {
            // Arrange
            var executor = _provider.GetRequiredService<Scenario>();

            // Act: Run scenario up to (but not including) ShipOrderStep
            var context = await executor.BuildAsync<OrderFulfillmentScenario>();

            // Assert: OrderId exists
            Assert.IsTrue(context.TryGet<Guid>("OrderId", out var orderId));
            Assert.AreNotEqual(Guid.Empty, orderId);
        }
    }
}