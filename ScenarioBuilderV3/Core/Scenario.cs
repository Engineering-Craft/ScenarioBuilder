using Microsoft.Extensions.DependencyInjection;
using ScenarioBuilderV3.Domain;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ScenarioBuilderV3.Core
{
    public class Scenario : IScenarioBuilder
    {
        private readonly ScenarioContext _context;
        private readonly List<ScenarioStep> _steps = new();
        private IServiceProvider _scenarioProvider;

        public Scenario()
        {
            _context = new ScenarioContext();
        }

        /// <summary>
        /// Add a step to the scenario pipeline.
        /// </summary>
        public IScenarioBuilder AddStep<TEvent>()
            where TEvent : IScenarioEvent
        {
            _steps.Add(new ScenarioStep(typeof(TEvent), () => _scenarioProvider.GetRequiredService<TEvent>()));
            return this;
        }

        /// <summary>
        /// Attribute-driven scenario execution.
        /// </summary>
        public async Task<ScenarioContext> BuildAsync<TScenario>(
            ScenarioExecutionOptions? options = null,
            CancellationToken ct = default)
            where TScenario : IScenario
        {
            // Build steps from attributes into this instance
            var attributeBuilder = new AttributeScenarioBuilder(this);
            attributeBuilder.Build<TScenario>();

            return await RunAsync(options, ct);
        }

        /// <summary>
        /// Builder-driven execution (fluent configuration).
        /// </summary>
        public async Task<ScenarioContext> BuildAsync<TScenario, TBuilder>(
            Func<TBuilder, TBuilder>? configure = null,
            CancellationToken ct = default)
            where TScenario : IScenario
            where TBuilder : class, IScenarioOptionsBuilder<TBuilder>, new()
        {
            var builder = new TBuilder();

            var configured = (configure ?? (b => b))(builder);

            _scenarioProvider = configured.Services;

            var options = configured.Options;

            var attributeBuilder = new AttributeScenarioBuilder(this);
            attributeBuilder.Build<TScenario>();

            return await RunAsync(options, ct);
        }

        /// <summary>
        /// Execute the scenario with optional execution options.
        /// </summary>
        public async Task<ScenarioContext> RunAsync(
            ScenarioExecutionOptions? options = null,
            CancellationToken ct = default)
        {
            options ??= ScenarioExecutionOptions.Default;

            foreach (var step in _steps)
            {
                if (options.ShouldStopBefore(step.StepId))
                    break;

                if (options.TryGetOverride(step.StepId, out var overrideEvent))
                {
                    await overrideEvent.ExecuteAsync(_context, ct);
                }
                else
                {
                    await step.Factory().ExecuteAsync(_context, ct);
                }
            }

            return _context;
        }

        private sealed record ScenarioStep(Type StepId, Func<IScenarioEvent> Factory);
    }
}