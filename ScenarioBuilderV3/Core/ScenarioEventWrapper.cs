using ScenarioBuilderV3.Core;

public sealed class ScenarioEvent<TScenario> : IScenarioEvent
    where TScenario : IAttributedScenario
{
    private readonly TScenario _scenario;
    private readonly IScenarioBuilder _builder;
    private readonly IServiceProvider _provider;

    public ScenarioEvent(TScenario scenario, IScenarioBuilder builder, IServiceProvider provider)
    {
        _scenario = scenario;
        _builder = builder;
        _provider = provider;
    }

    public async Task ExecuteAsync(ScenarioContext context, CancellationToken ct = default)
    {
        // Build the nested scenario using the factory-based AttributeScenarioBuilder
        var attributeBuilder = new AttributeScenarioBuilder(_builder, _provider);
        attributeBuilder.Build<TScenario>();

        // Run the steps in the current context
        await _builder.RunAsync(ct: ct);
    }
}