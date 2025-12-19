using ScenarioBuilderV2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV2.Core.BaseClasses
{
    public abstract class ScenarioBase<TContext> : IScenario<TContext>
    where TContext : class
    {
        protected abstract IEnumerable<IStep<TContext>> Steps { get; }

        protected virtual TContext CreateContext()
            => Activator.CreateInstance<TContext>();

        protected virtual void RegisterEvents(TContext context)
        { }

        public async Task<TContext> RunAsync(
        Action<ScenarioRunOptions<TContext>>? configure = null)
        {
            var options = new ScenarioRunOptions<TContext>();
            configure?.Invoke(options);

            var context = CreateContext();
            RegisterEvents(context);

            foreach (var step in Steps)
            {
                if (options.ShouldStopBefore(step))
                    break;

                await step.ExecuteAsync(context);
            }

            return context;
        }
    }
}