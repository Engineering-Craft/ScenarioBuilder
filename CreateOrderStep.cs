using System;


    public class CreateOrderStep : IStep<ScenarioContext>
    {
        public string Name => "Create order";

        public async Task ExecuteAsync(TestContext context)
        {
            var orderId = await context.Api.CreateOrderAsync();

            await context.Events.PublishAsync(
                new OrderCreated(orderId)
            );
        }
    }

