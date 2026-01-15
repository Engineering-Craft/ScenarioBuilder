using ScenarioBuilder.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Core.Interfaces
{
    // Domain/IScenarioEvent.cs
    public interface IScenarioEvent
    {
        Task ExecuteAsync(ScenarioBuilderContext context, CancellationToken ct = default);
    }
}