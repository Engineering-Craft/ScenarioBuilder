using Microsoft.Extensions.Logging;
using ScenarioBuilder;

namespace EwingJmScenarioBuilder
{
    /// <summary>
    /// An event for a user submitting an case.
    /// </summary>
    /// <param name="eventId">The event ID.</param>
    /// <param name="clientFactory">The client factory.</param>
    /// <param name="logger">The logger</param>
    public class UserSubmitsCaseEvent(string eventId, IServiceClientFactory clientFactory, ILogger<UserSubmitsCaseEvent> logger)
        : Event(eventId)
    {
        //   private readonly IServiceClientFactory clientFactory = clientFactory;
        private readonly ILogger<UserSubmitsCaseEvent> logger = logger;

        //  private readonly CaseFaker caseFaker = new CaseFaker();

        /// <inheritdoc/>
        public override async Task ExecuteAsync(ScenarioContext context)
        {
            this.logger.LogInformation($"Submitting a case.");

            //  using var client = this.clientFactory.GetServiceClient(Persona.User);
            //  var caseId = await client.CreateAsync(caseFaker.Generate());

            this.logger.LogInformation($"Created case {1222}.");

            context.Set("CaseId", 1222);
        }
    }
}