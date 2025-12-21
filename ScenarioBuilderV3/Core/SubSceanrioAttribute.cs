using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV3.Core
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class SubScenarioAttribute : Attribute
    {
        public Type StepId { get; }

        public SubScenarioAttribute(Type stepId)
        {
            StepId = stepId;
        }
    }
}