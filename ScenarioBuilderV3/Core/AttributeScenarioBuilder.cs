using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ScenarioBuilderV3.Core
{
    public sealed class AttributeScenarioBuilder
    {
        private readonly IScenarioBuilder _builder;
        private readonly IServiceProvider _provider;

        public AttributeScenarioBuilder(IScenarioBuilder builder, IServiceProvider provider)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        // Build method — adds all steps (including nested steps) to the builder
        public void Build<TScenario>() where TScenario : IAttributedScenario
        {
            var metadata = _cache.GetOrAdd(typeof(TScenario), t => BuildMetadata(t, _provider));

            foreach (var step in metadata.Steps)
            {
                var method = typeof(IScenarioBuilder)
                    .GetMethod(nameof(IScenarioBuilder.AddStep))!
                    .MakeGenericMethod(step.StepId, step.EventType);

                method.Invoke(_builder, null);
            }
        }

        // Recursively builds metadata including nested scenario steps
        private static ScenarioMetadata BuildMetadata(Type scenarioType, IServiceProvider provider)
        {
            var steps = new List<ScenarioStepMetadata>();
            CollectSteps(scenarioType, provider, steps);
            return new ScenarioMetadata(steps);
        }

        private static void CollectSteps(Type scenarioType, IServiceProvider provider, List<ScenarioStepMetadata> steps)
        {
            foreach (var prop in scenarioType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                // Normal scenario step
                var stepAttr = prop.GetCustomAttribute<ScenarioStepAttribute>();
                if (stepAttr != null)
                {
                    steps.Add(new ScenarioStepMetadata(stepAttr.StepId, prop.PropertyType));
                }

                // Nested scenario — recursively add its steps
                var nestedAttr = prop.GetCustomAttribute<NestedScenarioAttribute>();
                if (nestedAttr != null)
                {
                    // Resolve instance via DI (if needed)
                    var nestedScenario = provider.GetRequiredService(nestedAttr.ScenarioType);

                    // Recursively collect steps
                    CollectSteps(nestedAttr.ScenarioType, provider, steps);
                }
            }
        }

        // Metadata classes
        private sealed record ScenarioMetadata(IReadOnlyList<ScenarioStepMetadata> Steps);
        private sealed record ScenarioStepMetadata(Type StepId, Type EventType);

        // Cache for scenario metadata
        private static readonly ConcurrentDictionary<Type, ScenarioMetadata> _cache = new();
    }
}