using ScenarioBuilder.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.DomainEvents
{
    public sealed record OrderCreated(OrderModel OrderModel) : IEvent;

    public sealed record UserCreated(string UserId) : IEvent;

    public sealed record PaymentProcessed(string PaymentId, string OrderId) : IEvent;
}