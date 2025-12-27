using Microsoft.Extensions.DependencyInjection;
using ScenarioBuilderV3.Domain;
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
            var builder = _provider.GetRequiredService<IScenarioBuilder>();
            var attributeBuilder = new AttributeScenarioBuilder(builder);

            attributeBuilder.Build<TScenario>();

            return await builder.RunAsync(options, ct);
        }

        /// <summary>
        /// Builder-driven execution (fluent configuration)
        /// </summary>
        public async Task<ScenarioContext> BuildAsync<TScenario, TBuilder>(
     Func<TBuilder, TBuilder> configure,
     CancellationToken ct = default)
     where TScenario : IScenario
     where TBuilder : class, IScenarioOptionsBuilder<TBuilder>
        {
            // 1️⃣ Resolve the main scenario builder (attribute-based)
            var scenarioBuilder = _provider.GetRequiredService<IScenarioBuilder>();

            // 2️⃣ Resolve your OrderScenarioBuilder from DI
            var orderBuilder = _provider.GetRequiredService<TBuilder>();

            // 3️⃣ Apply fluent configuration to produce ScenarioExecutionOptions
            var configuredOrderBuilder = configure(orderBuilder);
            var options = configuredOrderBuilder.Options;

            // 4️⃣ Build attribute-driven steps
            var attributeBuilder = new AttributeScenarioBuilder(scenarioBuilder);
            attributeBuilder.Build<TScenario>();

            // 5️⃣ Execute the scenario with your builder’s options
            return await scenarioBuilder.RunAsync(options, ct);
        }

        //public async Task<TContext> BuildAsync<TBuilder, TContext>(
        // Func<TBuilder, TBuilder> configure,
        // CancellationToken ct = default)
        // where TBuilder : class, IBuilder
        //{
        //    // Resolve the builder from DI
        //    var builder = _provider.GetRequiredService<TBuilder>();

        //    // Apply fluent configuration
        //    var configuredBuilder = configure(builder);

        //    // Build options
        //    var options = configuredBuilder;

        //    // Run scenario
        //    var context = await configuredBuilder.RunAsync(options, ct);

        //    return context as TContext ?? throw new InvalidOperationException("Context type mismatch");
        //}
    }
}