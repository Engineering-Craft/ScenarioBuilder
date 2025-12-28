using System;

namespace ScenarioBuilder.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class NestedScenarioAttribute(Type scenarioType) : Attribute
    {
        public Type ScenarioType { get; } = scenarioType ?? throw new ArgumentNullException(nameof(scenarioType));
        public Type? StepId { get; }
    }
}