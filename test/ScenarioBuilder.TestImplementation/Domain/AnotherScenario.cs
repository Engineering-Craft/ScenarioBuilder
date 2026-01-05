using ScenarioBuilder.Core.Attributes;
using ScenarioBuilder.Core.Interfaces;

namespace ScenarioBuilder.Domain
{
    [Scenario]
    public sealed class AnotherScenario : IScenario
    {
        [ScenarioStep(typeof(SubScenarioEvent))]
        public SubScenarioEvent? DoSomeSubActivity { get; init; }
    }
}