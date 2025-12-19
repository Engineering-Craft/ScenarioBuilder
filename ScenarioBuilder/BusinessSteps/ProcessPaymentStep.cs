using ScenarioBuilder.Core;
using ScenarioBuilder.DomainEvents;
using ScenarioBuilder.Services;
using System;

namespace ScenarioBuilder.Steps
{
    public sealed class ProcessPaymentStep : IStep<ScenarioContext>
    {
        private readonly IPaymentService paymentService;

        public string Name => "Process payment";

        public ProcessPaymentStep(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        public async Task ExecuteAsync(ScenarioContext context)
        {
            // 1. Consume OrderCreated event data
            var order = context.Require<OrderCreated>();

            await paymentService.ProcessAsync();

            // 3. Emit a new event for downstream steps
            await context.Events.PublishAsync(
                new PaymentProcessed("222222", order.OrderModel.OrderId)
            );
        }

        public bool PreConditionsSatisfied(ScenarioContext ctx)
        {
            return ctx.Require<OrderCreated>() != null;
        }
    }
}