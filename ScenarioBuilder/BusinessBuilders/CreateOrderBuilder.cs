using ScenarioBuilder.BusinessSteps;
using ScenarioBuilder.Core;
using ScenarioBuilder.DomainEvents;
using ScenarioBuilder.Pipelines;
using ScenarioBuilder.Services;
using ScenarioBuilder.Steps;

namespace ScenarioBuilder.BusinessBuilders
{
    public sealed class CreateOrderBuilder
    {
        private OrderCreationStep _pipeline = ctx => Task.CompletedTask;

        private readonly IPaymentService _paymentService;
        private readonly OrderModel _model;

        public CreateOrderBuilder(IPaymentService paymentService, OrderModel orderModel)
        {
            _paymentService = paymentService;
            _model = orderModel;
        }

        public CreateOrderBuilder InStatusCompleted()
            => Decorate(async (ctx, next) =>
            {
                _model.Status = "Completed";
                await next(ctx);
            });

        public CreateOrderBuilder AndAlreadyShipped()
            => Decorate(async (ctx, next) =>
            {
                _model.Shipped = true;
                await next(ctx);
            });

        private CreateOrderBuilder Decorate(
            Func<ScenarioContext, OrderCreationStep, Task> decorator)
        {
            var next = _pipeline;

            _pipeline = ctx => decorator(ctx, next);
            return this;
        }

        public ActivateOrderStep Build()
            => new ActivateOrderStep();

        public OrderDocumentProcessorBuilder BySettingTheDocuments(Action<OrderDocumentProcessorBuilder> configure)
        {
            // create the sub-builder, passing in the same order model
            var subBuilder = new OrderDocumentProcessorBuilder(_model);

            // allow caller to configure the sub-builder
            configure(subBuilder);

            // decorate the parent pipeline to execute the sub-builder
            _pipeline = async ctx =>
            {
                // first execute parent pipeline
                await _pipeline(ctx);

                // then execute the sub-builder
                // await subBuilder.Build();
            };

            // return the sub-builder for further fluent chaining
            return subBuilder;
        }
    }
}