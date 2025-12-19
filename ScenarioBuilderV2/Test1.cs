using Microsoft.Extensions.DependencyInjection;
using ScenarioBuilderV2.Scenarios;
using ScenarioBuilderV2.Scenarios.Steps;

namespace ScenarioBuilderV2
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public async Task ScenarioComplex()
        {
            var _services = new ServiceCollection();

            // Steps
            _services.AddTransient<ProcessPaymentStep>();
            _services.AddTransient<VerifyOrderStep>();
            _services.AddTransient<CreateCompletedPaidOrderStep>();
            _services.AddScoped<CompletedPaidOrderScenario>();

            var provider = _services.BuildServiceProvider();

            var res = await provider.GetRequiredService<CompletedPaidOrderScenario>()
                                                .RunAsync(o => o.RunUntil<VerifyOrderStep>());

            Assert.IsTrue(res.Events.GetHandlers().Any());
        }
    }
}