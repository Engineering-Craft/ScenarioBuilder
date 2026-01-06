using Bogus;
using ScenarioBuilder.Domain;
using ScenarioBuilder.Tests.ScenarioBuilder.TestImplementation.Domain.Model;
using ScenarioBuilder.Tests.ScenarioBuilder.TestImplementation.Domain.Model.Fakers;

namespace ScenarioBuilder.Tests.Integration
{
    [TestClass]
    public class OrderFulfillmentScenarioTests
    {
        [TestMethod]
        public async Task Scenario_Overrides_Step_And_Runs_Until()
        {
            // Arrange
            var scenario = new OrderScenario();

            // Act: Run scenario up to (but not including) ShipOrderStep
            var builtScenario = await scenario.ExecuteAsync<OrderScenarioBuilder>
                                                    (
                                                        b => b.ByFailingPayment()
                                                    );
            // Assert: OrderId exists
            Assert.IsTrue(builtScenario.GetContext().TryGet<Guid>("OrderId", out var orderId));
            Assert.AreNotEqual(Guid.Empty, orderId);

            Assert.IsFalse(builtScenario.GetContext().TryGet<Guid>("PaymentId", out _));
        }

        [TestMethod]
        public async Task Scenario_Runs_Through()
        {
            // Arrange
            var scenario = new OrderScenario();

            // Act: Run scenario up to (but not including) ShipOrderStep
            var builtScenario = await scenario.ExecuteAsync<OrderScenarioBuilder>();

            // Assert: OrderId exists
            Assert.IsTrue(builtScenario.GetContext().TryGet<Guid>("OrderId", out var orderId));
            Assert.AreNotEqual(Guid.Empty, orderId);

            Assert.IsTrue(builtScenario.GetContext().TryGet<Guid>("PaymentId", out _));
        }

        [TestMethod]
        public async Task Scenario_Uses_InjectedObjects()
        {
            // Arrange
            var scenario = new OrderScenario();

            var orderFaked = new OrderFaker().Generate();
            var shippingFaked = new ShippingFaker().Generate();

            // Act: Run scenario up to (but not including) ShipOrderStep
            var builtScenario = await scenario.ExecuteAsync<OrderScenarioBuilder>
                                                   (
                                                       b => b.BySettingTheOrder(orderFaked)
                                                             .BySettingTheShipping(shippingFaked)

                                                   );

            // Assert: OrderId exists
            Assert.IsTrue(builtScenario.GetContext().TryGet<Guid>("OrderId", out var orderId));
            Assert.AreEqual(orderFaked.Id, orderId);
        }
    }
}