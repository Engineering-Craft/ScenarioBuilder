namespace ScenarioBuilder.Core
{
    public interface IStep<TContext>
    {
        string Name { get; }

        Task ExecuteAsync(TContext context);

        bool PreConditionsSatisfied(TContext context);
    }
}