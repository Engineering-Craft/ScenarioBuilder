using Microsoft.Extensions.DependencyInjection;
using ScenarioBuilder.BusinessFactories;
using ScenarioBuilder.BusinessSteps;
using ScenarioBuilder.Core;
using ScenarioBuilder.Services;
using ScenarioBuilder.Steps;

namespace ScenarioBuilder
{
    public class Tests
    {
        private ServiceProvider provider;

        [SetUp]
        public void SetUp()
        {
            var _services = new ServiceCollection();
            _services.AddScoped<IPaymentService, PaymentService>();

            // Steps
            _services.AddTransient<ProcessPaymentStep>();
            _services.AddTransient<ActivateOrderStep>();
            _services.AddTransient<CreateOrderStep>();
            _services.AddTransient<CreateCompletedPaidOrderStep>();

            _services.AddScoped<CreateOrderFactory>();

            provider = _services.BuildServiceProvider();
        }

        [TearDown]
        public void Dispose()
        {
            provider.Dispose();
        }

        [Test]
        public async Task SequentialSteps()
        {
            var scenario = ScenarioBuilder<ScenarioContext>.Create()
                                                        .Given(provider.GetRequiredService<CreateOrderStep>().WithUnpaidStatus())
                                                        .When(provider.GetRequiredService<ProcessPaymentStep>())
                                                        .Then(new VerifyOrderStep())
                                                        .Build();

            var context = new ScenarioContext();
            ContextEventBindings.Register(context);
            await scenario.RunAsync(context);
        }

        [Test]
        public async Task CompletedPaidOrder_Composite()
        {
            var scenario = ScenarioBuilder<ScenarioContext>.Create()
                                                        .Given(provider.GetRequiredService<CreateCompletedPaidOrderStep>())
                                                        .When(provider.GetRequiredService<ProcessPaymentStep>())
                                                        .Then(new VerifyOrderStep())
                                                        .Build();

            var context = new ScenarioContext();
            ContextEventBindings.Register(context);
            await scenario.RunAsync(context);
        }
    }
}