using ScenarioBuilder.Core;
using ScenarioBuilder.Domain;

namespace ScenarioEngine.Tests
{
    [TestClass]
    public class OrderFulfillmentScenarioTests
    {
        [TestMethod]
        public async Task RunScenarioThroughToPaymentStep()
        {
            // Arrange
            var scenario = new OrderScenario();

            // Act: Run scenario up to (but not including) ShipOrderStep
            var builtScenario = await scenario.BuildAsync<OrderScenarioBuilder>
                                                    (
                                                        b => b.ByFailingPayment()
                                                              .BySettingTheShipping(new Shipping() { Name = "Test", Method = "Air" })
                                                    );

            // Assert: OrderId exists
            Assert.IsTrue(builtScenario.GetContext().TryGet<Guid>("OrderId", out var orderId));
            Assert.AreNotEqual(Guid.Empty, orderId);

            Assert.IsFalse(builtScenario.GetContext().TryGet<Guid>("PaymentId", out var paymentId));
        }

        [TestMethod]
        public async Task RunFullScenario()
        {
            // Arrange
            var scenario = new OrderScenario();

            // Act: Run scenario up to (but not including) ShipOrderStep
            var builtScenario = await scenario.BuildAsync<OrderScenarioBuilder>();

            // Assert: OrderId exists
            Assert.IsTrue(builtScenario.GetContext().TryGet<Guid>("OrderId", out var orderId));
            Assert.AreNotEqual(Guid.Empty, orderId);

            Assert.IsTrue(builtScenario.GetContext().TryGet<Guid>("PaymentId", out var paymentId));
        }
    }
}