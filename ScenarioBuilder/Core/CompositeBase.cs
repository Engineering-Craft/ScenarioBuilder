using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Core
{
    public abstract class CompositeStepBase<TContext> : ICompositeStep<TContext>
    {
        protected readonly List<IStep<TContext>> _steps = new();

        public IReadOnlyCollection<IStep<TContext>> Steps => _steps;

        public string Name => "";

        protected void AddStep(IStep<TContext> step)
        {
            _steps.Add(step);
        }

        public async Task ExecuteAsync(TContext context)
        {
            foreach (var step in _steps)
            {
                await step.ExecuteAsync(context);
            }
        }

        public bool PreConditionsSatisfied(TContext context)
        {
            return true;
        }
    }
}