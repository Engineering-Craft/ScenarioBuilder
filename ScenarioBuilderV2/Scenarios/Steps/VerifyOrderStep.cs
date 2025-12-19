using ScenarioBuilder.Core;
using ScenarioBuilderV2.Core.Interfaces;

namespace ScenarioBuilderV2.Scenarios.Steps
{
    public class VerifyOrderStep : IStep<ScenarioContext>
    {
        public async Task ExecuteAsync(ScenarioContext context)
        {
            await Task.Delay(1000);
        }
    }
}