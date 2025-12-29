using ScenarioBuilder.Core.Interfaces;
using ScenarioBuilder.Core;

namespace ScenarioBuilder.Domain
{
    // Domain/Events.cs
    public sealed class CreateOrderEvent : IScenarioEvent
    {
        private readonly IPaymentService ps;

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
            context.Set<Guid>("PaymentId", Guid.NewGuid());
            return Task.CompletedTask;
        }
    }

    internal class ChargePaymentEventFail : IScenarioEvent
    {
        private readonly IPaymentService ps;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public ChargePaymentEventFail()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
        }

        public ChargePaymentEventFail(IPaymentService svc)
        {
            this.ps = svc;
        }

        public Task ExecuteAsync(ScenarioContext context, CancellationToken ct = default)
        {
            ps.Pay();
            return Task.CompletedTask;
        }
    }

    public sealed class VerifyPaymentEvent : IScenarioEvent
    {
        public Task ExecuteAsync(ScenarioContext context, CancellationToken ct = default)
        {
            Console.WriteLine("Payment Verified");
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