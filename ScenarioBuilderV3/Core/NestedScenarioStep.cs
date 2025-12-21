using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV3.Core
{
    public sealed class NestedScenarioStep<TScenario> : IScenarioStepId
     where TScenario : IAttributedScenario
    {
    }
}