using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV3.Core
{
    // Infrastructure/ScenarioExecutor.cs
    public sealed class ScenarioExecutor
    {
        private readonly IServiceProvider _provider;

        public ScenarioExecutor(IServiceProvider provider) => _provider = provider;

        public async Task<ScenarioContext> BuildAsync<TScenario>(
            ScenarioExecutionOptions? options = null,
            CancellationToken ct = default)
            where TScenario : IAttributedScenario
        {
            var context = _provider.GetRequiredService<ScenarioContext>();
            var builder = _provider.GetRequiredService<IScenarioBuilder>();
            var attributeBuilder = new AttributeScenarioBuilder(builder, _provider);

            attributeBuilder.Build<TScenario>();
            return await builder.RunAsync(options, ct);
        }
    }
}