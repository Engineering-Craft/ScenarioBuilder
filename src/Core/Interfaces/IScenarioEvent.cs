using ScenarioBuilder.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Core.Interfaces
{
    // Domain/IScenarioEvent.cs
    public interface IScenarioEvent
    {
        Task ExecuteAsync(ScenarioContext context, CancellationToken ct = default);
    }
}