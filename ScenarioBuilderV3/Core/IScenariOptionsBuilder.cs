using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV3.Core
{
    public interface IScenarioOptionsBuilder<T> where T : class
    {
        ScenarioExecutionOptions Options { get; }

        IServiceProvider Services { get; }
    }
}