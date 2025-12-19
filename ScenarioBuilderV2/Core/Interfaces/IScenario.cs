using ScenarioBuilderV2.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV2.Core.Interfaces
{
    public interface IScenario<TContext>
    {
        Task<TContext> RunAsync(Action<ScenarioRunOptions<TContext>>? configure = null);
    }
}