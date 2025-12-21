using ScenarioBuilderV3.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV3.Domain
{
    // Domain/Events.cs
    public sealed class CreateOrderEvent : IScenarioEvent
    {
        private IPaymentService ps;

        public CreateOrderEvent(IPaymentService svc)
        {
            this.ps = svc;
        }

        public Task ExecuteAsync(ScenarioContext context, CancellationToken ct = default)
        {
            ps.Pay();
            var orderId = Guid.NewGuid();
            context.Set("OrderId", orderId);
            Console.WriteLine($"Order created: {orderId}");
            return Task.CompletedTask;
        }
    }

    public sealed class ReserveInventoryEvent : IScenarioEvent
    {
        public Task ExecuteAsync(ScenarioContext context, CancellationToken ct = default)
        {
            Console.WriteLine("Inventory reserved");
            return Task.CompletedTask;
        }
    }

    public sealed class ChargePaymentEvent : IScenarioEvent
    {
        public Task ExecuteAsync(ScenarioContext context, CancellationToken ct = default)
        {
            Console.WriteLine("Payment charged");
            return Task.CompletedTask;
        }
    }

    public sealed class ShipOrderEvent : IScenarioEvent
    {
        public Task ExecuteAsync(ScenarioContext context, CancellationToken ct = default)
        {
            Console.WriteLine("Order shipped");
            return Task.CompletedTask;
        }
    }
}