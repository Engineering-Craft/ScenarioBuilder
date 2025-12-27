using Microsoft.Extensions.DependencyInjection;
using ScenarioBuilderV3.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV3.Core
{
    // Infrastructure/ScenarioBuilder.cs
    public class ScenarioBuilder : IScenarioBuilder
    {
        private readonly IServiceProvider _provider;
        private readonly ScenarioContext _context;
        private readonly List<ScenarioStep> _steps = new();

        public ScenarioBuilder(IServiceProvider provider, ScenarioContext context)
        {
            _provider = provider;
            _context = context;
        }

        public IScenarioBuilder AddStep<TEvent>()
            where TEvent : IScenarioEvent
        {
            _steps.Add(new ScenarioStep(typeof(TEvent), () => _provider.GetRequiredService<TEvent>()));
            return this;
        }

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
                    await overrideEvent.ExecuteAsync(_context, ct);
                else
                    await step.Factory().ExecuteAsync(_context, ct);
            }

            return _context;
        }

        private sealed record ScenarioStep(Type StepId, Func<IScenarioEvent> Factory);
    }
}