using System;
using System.Threading;
using System.Threading.Tasks;

namespace ScenarioBuilderV3.Core
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

        /// <summary>
        /// Runs all steps in the scenario sequentially.
        /// </summary>
        /// <param name="options">Optional execution options (e.g., StopBefore).</param>
        /// <param name="ct">Optional cancellation token.</param>
        Task<ScenarioContext> RunAsync(ScenarioExecutionOptions? options = null, CancellationToken ct = default);
    }
}