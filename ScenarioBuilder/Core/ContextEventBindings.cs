using ScenarioBuilder.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Core
{
    public static class ContextEventBindings
    {
        public static void Register(ScenarioContext context)
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
    }
}