using Microsoft.Extensions.DependencyInjection;
using ScenarioBuilder.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ScenarioBuilder.Core.Interfaces
{
    /// <summary>
    /// Builds and executes a scenario consisting of multiple steps.
    /// Supports nested scenarios and factory-based step creation.
    /// </summary>
    public interface IScenarioBuilder
    {
        /// <summary>
        /// Adds a step by type. Not used for nested scenarios (see factory overload).
        /// </summary>
        /// <typeparam name="TEvent">The event type to execute.</typeparam>
        IScenarioBuilder AddStep<TEvent>()
            where TEvent : IScenarioEvent;
    }
}