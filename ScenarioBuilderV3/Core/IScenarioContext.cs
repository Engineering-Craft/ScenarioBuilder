using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV3.Core
{
    // Domain/IScenarioEvent.cs
    public interface IScenarioEvent
    {
        Task ExecuteAsync(ScenarioContext context, CancellationToken ct = default);
    }
}