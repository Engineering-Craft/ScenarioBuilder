using ScenarioBuilderV3.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV3.Domain
{
    public sealed class CreateOrderStep : IScenarioStepId
    { }

    public sealed class ReserveInventoryStep : IScenarioStepId
    { }

    public sealed class ChargePaymentStep : IScenarioStepId
    { }

    public sealed class VerifyPaymentStep : IScenarioStepId
    { }

    public sealed class ShipOrderStep : IScenarioStepId
    { }
}