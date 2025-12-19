using ScenarioBuilder.Core;
using ScenarioBuilder.DomainEvents;
using ScenarioBuilder.Pipelines;

namespace ScenarioBuilder.Steps
{
    public sealed class ProcessOrderStep : IStep<ScenarioContext>
    {
        public string Name => "Process order";

        private readonly OrderCreationStep _pipeline;
        private OrderModel _orderModel = new();

        public async Task ExecuteAsync(ScenarioContext context)
        {
            await context.Events.PublishAsync(
                new OrderCreated(_orderModel)
            );
        }

        internal IStep<ScenarioContext> WithPaidStatus()
        {
            _orderModel.Status = "Paid";
            return this;
        }

        public bool PreConditionsSatisfied(ScenarioContext ctx)
        {
            return true;
        }
    }
}