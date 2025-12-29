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
            var scenario = new OrderScenario();

            // Act: Run scenario up to (but not including) ShipOrderStep
            await scenario.BuildAsync<OrderScenarioBuilder>
                                                    (
                                                        b => b.ByFailingPayment()
                                                              .BySettingTheShipping()
                                                    );

            // Assert: OrderId exists
            Assert.IsTrue(scenario.GetContext().TryGet<Guid>("OrderId", out var orderId));
            Assert.AreNotEqual(Guid.Empty, orderId);

            Assert.IsFalse(scenario.GetContext().TryGet<Guid>("PaymentId", out var paymentId));

            await scenario.BuildAsync<OrderScenarioBuilder>
                                                   (b => b.BySettingTheShipping()
                                                   );
        }

        [TestMethod]
        public async Task RunFullScenario()
        {
            // Arrange
            var scenario = new OrderScenario();

            // Act: Run scenario up to (but not including) ShipOrderStep
            var context = await scenario.BuildAsync<OrderScenarioBuilder>();

            // Assert: OrderId exists
            Assert.IsTrue(context.TryGet<Guid>("OrderId", out var orderId));
            Assert.AreNotEqual(Guid.Empty, orderId);

            Assert.IsTrue(context.TryGet<Guid>("PaymentId", out var paymentId));
        }
    }
}