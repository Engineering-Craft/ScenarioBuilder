using ScenarioBuilder.Core;
using ScenarioBuilder.DomainEvents;
using ScenarioBuilder.Pipelines;

namespace ScenarioBuilder.Steps
{
    public sealed class CreateOrderStep : IStep<ScenarioContext>
    {
        public string Name => "Create order";

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

        internal IStep<ScenarioContext> WithUnpaidStatus()
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