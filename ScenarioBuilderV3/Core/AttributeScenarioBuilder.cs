using System.Collections.Concurrent;
using System.Reflection;

namespace ScenarioBuilderV3.Core
{
    public sealed class AttributeScenarioBuilder
    {
        private readonly IScenarioBuilder _builder;

        public AttributeScenarioBuilder(IScenarioBuilder builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        // Build method — adds all steps (including nested steps) to the builder
        public void Build<TScenario>() where TScenario : IAttributedScenario
        {
            var metadata = _cache.GetOrAdd(typeof(TScenario), t => BuildMetadata(t));

            foreach (var step in metadata.Steps)
            {
                var method = typeof(IScenarioBuilder)
                    .GetMethod(nameof(IScenarioBuilder.AddStep))!
                    .MakeGenericMethod(step.EventType);

                method.Invoke(_builder, null);
            }
        }

        private static ScenarioMetadata BuildMetadata(Type scenarioType)
        {
            var steps = new List<ScenarioStepMetadata>();
            CollectStepsRecursive(scenarioType, steps);
            return new ScenarioMetadata(steps);
        }

        private static void CollectStepsRecursive(
            Type scenarioType,
            List<ScenarioStepMetadata> steps)
        {
            foreach (var prop in scenarioType.GetProperties(
                BindingFlags.Public | BindingFlags.Instance))
            {
                // Normal step
                var stepAttr = prop.GetCustomAttribute<ScenarioStepAttribute>();
                if (stepAttr != null)
                {
                    steps.Add(new ScenarioStepMetadata(
                        stepAttr.StepId,
                        prop.PropertyType));
                    continue;
                }

                // Nested scenario → recurse
                var nestedAttr = prop.GetCustomAttribute<NestedScenarioAttribute>();
                if (nestedAttr != null)
                {
                    CollectStepsRecursive(nestedAttr.ScenarioType, steps);
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