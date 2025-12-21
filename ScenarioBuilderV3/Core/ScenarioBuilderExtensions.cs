//using Microsoft.Extensions.DependencyInjection;
//using ScenarioBuilderV3.Core;

//public static class ScenarioBuilderExtensions
//{
//    public static IScenarioBuilder ExecuteScenario<TScenario>(
//        this IScenarioBuilder builder,
//        TScenario scenario,
//        IServiceProvider provider)
//        where TScenario : IAttributedScenario
//    {
//        // Factory lambda creates the ScenarioEvent<TScenario> at runtime
//        builder.AddStep<NestedScenarioStep<TScenario>, ScenarioEvent<TScenario>>(
//            () => new ScenarioEvent<TScenario>(scenario, builder, provider)
//        );

//        return builder;
//    }

//    // Overload to resolve the scenario from DI
//    public static IScenarioBuilder ExecuteScenario<TScenario>(
//        this IScenarioBuilder builder,
//        IServiceProvider provider)
//        where TScenario : IAttributedScenario
//    {
//        var scenario = provider.GetRequiredService<TScenario>();
//        return builder.ExecuteScenario(scenario, provider);
//    }
//}