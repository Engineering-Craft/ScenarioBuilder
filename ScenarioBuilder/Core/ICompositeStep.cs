namespace ScenarioBuilder.Core
{
    public interface ICompositeStep<TContext> : IStep<TContext>
    {
        IReadOnlyCollection<IStep<TContext>> Steps { get; }
    }
}