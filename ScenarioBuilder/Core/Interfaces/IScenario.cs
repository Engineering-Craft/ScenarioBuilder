using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderv1.Core.Interfaces
{
    public interface IScenario<TContext>
    {
        Task RunAsync(TContext context);
    }
}