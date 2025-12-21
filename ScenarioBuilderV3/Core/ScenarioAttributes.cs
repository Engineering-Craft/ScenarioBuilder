using ScenarioBuilderV3.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV3.Core
{
    // Domain/ScenarioAttributes.cs
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ScenarioAttribute : Attribute
    { }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ScenarioStepAttribute : Attribute
    {
        public Type StepId { get; }

        public ScenarioStepAttribute(Type stepId)
        {
            if (!typeof(IScenarioEvent).IsAssignableFrom(stepId))
                throw new ArgumentException("StepId must implement IScenarioEvent");
            StepId = stepId;
        }
    }
}