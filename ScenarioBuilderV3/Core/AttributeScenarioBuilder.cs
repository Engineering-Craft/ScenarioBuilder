using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ScenarioBuilderV3.Core
{
    // Infrastructure/AttributeScenarioBuilder.cs
    public sealed class AttributeScenarioBuilder
    {
        private readonly IScenarioBuilder _builder;
        private readonly IServiceProvider _provider;

        public AttributeScenarioBuilder(IScenarioBuilder builder, IServiceProvider provider)
        {
            _builder = builder;
            _provider = provider;
        }

        // Build method remains the same
        public void Build<TScenario>() where TScenario : IAttributedScenario
        {
            var metadata = _cache.GetOrAdd(typeof(TScenario), BuildMetadata);

            foreach (var step in metadata.Steps)
            {
                var method = typeof(IScenarioBuilder)
                    .GetMethod(nameof(IScenarioBuilder.AddStep))!
                    .MakeGenericMethod(step.StepId, step.EventType);

                method.Invoke(_builder, null);
            }
        }

        private static ScenarioMetadata BuildMetadata(Type scenarioType)
        {
            var steps = scenarioType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p =>
                {
                    var stepAttr = p.GetCustomAttribute<ScenarioStepAttribute>();
                    var subAttr = p.GetCustomAttribute<SubScenarioAttribute>();
                    var stepId = stepAttr?.StepId ?? subAttr?.StepId;
                    return stepId is null ? null : new ScenarioStepMetadata(stepId, p.PropertyType);
                })
                .Where(x => x != null)
                .Select(x => x!)
                .ToList();

            return new ScenarioMetadata(steps);
        }

        private sealed record ScenarioMetadata(IReadOnlyList<ScenarioStepMetadata> Steps);
        private sealed record ScenarioStepMetadata(Type StepId, Type EventType);

        private static readonly ConcurrentDictionary<Type, ScenarioMetadata> _cache = new();
    }
}