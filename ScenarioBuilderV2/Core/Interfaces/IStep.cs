namespace ScenarioBuilderV2.Core.Interfaces
{
    public interface IStep<TContext>
    {
        Task ExecuteAsync(TContext context);
    }
}