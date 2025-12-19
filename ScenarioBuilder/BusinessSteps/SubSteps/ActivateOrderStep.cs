using ScenarioBuilder.Core;
using ScenarioBuilder.DomainEvents;
using ScenarioBuilder.Pipelines;

namespace ScenarioBuilder.Steps
{
    public sealed class ActivateOrderStep : IStep<ScenarioContext>
    {
        public string Name => "Activate order";

        private readonly OrderCreationStep _pipeline;
        private OrderModel _orderModel = new();

        //internal CreateOrderStep(OrderCreationStep pipeline)
        //{
        //    _pipeline = pipeline;
        //    this._orderModel = new OrderModel();
        //}

        public async Task ExecuteAsync(ScenarioContext context)
        {
            await context.Events.PublishAsync(
                new OrderCreated(_orderModel)
            );

            //  await _pipeline(context);
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