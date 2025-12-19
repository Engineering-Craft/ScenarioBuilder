using ScenarioBuilder.Core;
using ScenarioBuilder.DomainEvents;
using ScenarioBuilderV2.Core.Interfaces;

namespace ScenarioBuilderV2.Scenarios.Steps
{
    public class ProcessPaymentStep : IStep<ScenarioContext>
    {
        public async Task ExecuteAsync(ScenarioContext context)
        {
            await Task.Delay(1000);
            await context.Events.PublishAsync(
               new PaymentProcessed("222222", "111")
           );
        }
    }
}