using ScenarioBuilder.BusinessBuilders;
using ScenarioBuilder.Core;
using ScenarioBuilder.Pipelines;
using ScenarioBuilder.Services;
using ScenarioBuilder.Steps;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.BusinessSteps
{
    public sealed class CreateCompletedPaidOrderStep
    : CompositeStepBase<ScenarioContext>
    {
        public CreateCompletedPaidOrderStep(
            IPaymentService paymentService)
        {
            var model = new OrderModel();

            AddStep(
                new CreateOrderBuilder(paymentService, model)
                    .InStatusCompleted()
                    .Build());

            AddStep(
                new OrderDocumentProcessorBuilder(model)
                    .WithDocument("Invoice")
                    .Build());
        }
    }
}