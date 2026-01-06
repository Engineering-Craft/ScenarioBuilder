using ScenarioBuilder.Core.Interfaces;
using ScenarioBuilder.Core;
using ScenarioBuilder.Tests.ScenarioBuilder.TestImplementation.Domain.Model;

namespace ScenarioBuilder.Domain
{
    // Domain/Events.cs
    public sealed class CreateOrderEvent : IScenarioEvent
    {
        public CreateOrderEvent()
        {
        }

        public Task ExecuteAsync(ScenarioContext context, CancellationToken ct = default)
        {
            var order = context.Get<Order>(nameof(Order));

            context.Set("OrderId", order.Id);
            Console.WriteLine($"Order created: {order.Id}");
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
        private readonly IPaymentService ps;

        public ChargePaymentEvent(IPaymentService svc)
        {
            this.ps = svc;
        }

        public Task ExecuteAsync(ScenarioContext context, CancellationToken ct = default)
        {
            ps.Pay();
            Console.WriteLine("Payment charged");
            context.Set<Guid>("PaymentId", Guid.NewGuid());
            return Task.CompletedTask;
        }
    }

    internal class ChargePaymentEventFail : IScenarioEvent
    {
        private readonly IPaymentService? ps;

        public ChargePaymentEventFail()
        {
        }

        public ChargePaymentEventFail(IPaymentService svc)
        {
            this.ps = svc;
        }

        public Task ExecuteAsync(ScenarioContext context, CancellationToken ct = default)
        {
            ps?.Pay();
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
            Shipping obj = context.Get<Shipping>(nameof(Shipping));

            Console.WriteLine($"Shipping with {obj.Name} by {obj.Method}");
            Console.WriteLine("Order shipped");
            return Task.CompletedTask;
        }
    }

    public sealed class SubScenarioEvent : IScenarioEvent
    {
        public Task ExecuteAsync(ScenarioContext context, CancellationToken ct = default)
        {
            Console.WriteLine("SubScenario executed");
            return Task.CompletedTask;
        }
    }
}