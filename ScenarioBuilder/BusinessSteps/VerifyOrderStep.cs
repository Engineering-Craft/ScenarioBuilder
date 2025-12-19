using ScenarioBuilder.Core;
using ScenarioBuilder.DomainEvents;

namespace ScenarioBuilder.Steps
{
    public sealed class VerifyOrderStep : IStep<ScenarioContext>
    {
        public string Name => "Verify order exists";

        public async Task ExecuteAsync(ScenarioContext context)
        {
            await Task.Delay(1000);
        }

        public bool PreConditionsSatisfied(ScenarioContext ctx)
        {
            return ctx.Require<OrderCreated>() != null;
        }
    }
}