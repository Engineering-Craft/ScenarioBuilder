using System;

namespace ScenarioBuilderV3.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class NestedScenarioAttribute : Attribute
    {
        public Type ScenarioType { get; }
        public Type StepId { get; }

        public NestedScenarioAttribute(Type scenarioType)
        {
            ScenarioType = scenarioType ?? throw new ArgumentNullException(nameof(scenarioType));
        }
    }
}