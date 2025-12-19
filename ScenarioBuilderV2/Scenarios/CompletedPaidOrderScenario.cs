using ScenarioBuilder.Core;
using ScenarioBuilder.DomainEvents;
using ScenarioBuilderV2.Core.BaseClasses;
using ScenarioBuilderV2.Core.Interfaces;
using ScenarioBuilderV2.Scenarios.Steps;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV2.Scenarios
{
    public sealed class CompletedPaidOrderScenario : ScenarioBase<ScenarioContext>
    {
        private readonly CreateCompletedPaidOrderStep _createOrder;
        private readonly ProcessPaymentStep _processPayment;
        private readonly VerifyOrderStep _verify;

        public CompletedPaidOrderScenario(
            CreateCompletedPaidOrderStep createOrder,
            ProcessPaymentStep processPayment,
            VerifyOrderStep verify)
        {
            _createOrder = createOrder;
            _processPayment = processPayment;
            _verify = verify;
        }

        protected override void RegisterEvents(ScenarioContext context)
        {
            context.Events.Subscribe<OrderCreated>(e =>
            {
                context.Set(e);
                return Task.CompletedTask;
            });

            context.Events.Subscribe<UserCreated>(e =>
            {
                context.Set(e);
                return Task.CompletedTask;
            });

            context.Events.Subscribe<PaymentProcessed>(e =>
            {
                context.Set(e);
                return Task.CompletedTask;
            });
        }

        protected override IEnumerable<IStep<ScenarioContext>> Steps
            => new IStep<ScenarioContext>[]
            {
            _createOrder,
            _processPayment,
            _verify
            };
    }
}