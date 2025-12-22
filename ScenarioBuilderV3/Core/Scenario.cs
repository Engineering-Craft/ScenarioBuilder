using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV3.Core
{
    // Infrastructure/ScenarioExecutor.cs
    public sealed class Scenario
    {
        private readonly IServiceProvider _provider;

        public Scenario(IServiceProvider provider) => _provider = provider;

        public async Task<ScenarioContext> BuildAsync<TScenario>(
            ScenarioExecutionOptions? options = null,
            CancellationToken ct = default)
            where TScenario : IScenario
        {
            var context = _provider.GetRequiredService<ScenarioContext>();
            var builder = _provider.GetRequiredService<IScenarioBuilder>();
            var attributeBuilder = new AttributeScenarioBuilder(builder);

            attributeBuilder.Build<TScenario>();
            return await builder.RunAsync(options, ct);
        }

        public async Task<AttributeScenarioBuilder> Build<TScenario>(
            ScenarioExecutionOptions? options = null,
            CancellationToken ct = default)
            where TScenario : IScenario
        {
            var context = _provider.GetRequiredService<ScenarioContext>();
            var builder = _provider.GetRequiredService<IScenarioBuilder>();
            var attributeBuilder = new AttributeScenarioBuilder(builder);

            attributeBuilder.Build<TScenario>();
            return attributeBuilder;
        }
    }
}