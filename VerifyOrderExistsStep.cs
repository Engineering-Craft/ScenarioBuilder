using System;

namespace Steps
{
    public sealed class VerifyOrderStep : IStep<TestContext>
    {
        public string Name => "Verify order exists";

        public async Task ExecuteAsync(TestContext context)
        {
            var orderCreated = context.Get<OrderCreated>();

            var exists = await context.Api.OrderExistsAsync(orderCreated.OrderId);

            if (!exists)
                throw new InvalidOperationException("Order not found");
        }
    }
}