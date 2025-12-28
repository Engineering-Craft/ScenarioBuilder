using Microsoft.Extensions.DependencyInjection;
using ScenarioBuilder.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Core.Interfaces
{
    public interface IScenarioOptionsBuilder
    {
        ScenarioExecutionOptions Options { get; }

        IServiceProvider Services { get; }
    }
}