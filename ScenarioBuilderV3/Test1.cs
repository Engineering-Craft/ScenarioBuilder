using ScenarioBuilder.Core;
using ScenarioBuilderV3.Domain;

namespace ScenarioEngine.Tests
{
    [TestClass]
    public class OrderFulfillmentScenarioTests
    {
        [TestMethod]
        public async Task RunScenarioThroughToPaymentStep()
        {
            // Arrange
            var scenario = new Scenario();

            // Act: Run scenario up to (but not including) ShipOrderStep
            var context = await scenario.BuildAsync<OrderScenario, OrderScenarioBuilder>
                                                    (
                                                        b => b.ByFailingPayment()
                                                              .BySettingTheShipping()
                                                    );

            // Assert: OrderId exists
            Assert.IsTrue(context.TryGet<Guid>("OrderId", out var orderId));
            Assert.AreNotEqual(Guid.Empty, orderId);

            Assert.IsFalse(context.TryGet<Guid>("PaymentId", out var paymentId));

            // Optional: could check side effects if events updated context
            Console.WriteLine($"Order ran through payment: {orderId}");
        }

        [TestMethod]
        public async Task RunFullScenario()
        {
            // Arrange
            var scenario = new Scenario();

            // Act: Run scenario up to (but not including) ShipOrderStep
            var context = await scenario.BuildAsync<OrderScenario, OrderScenarioBuilder>();

            // Assert: OrderId exists
            Assert.IsTrue(context.TryGet<Guid>("OrderId", out var orderId));
            Assert.AreNotEqual(Guid.Empty, orderId);

            Assert.IsTrue(context.TryGet<Guid>("PaymentId", out var paymentId));
        }
    }
}