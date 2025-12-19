using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Core
{
    public sealed class Scenario<TContext>
    {
        private readonly IReadOnlyList<IStep<TContext>> _steps;

        public Scenario(IEnumerable<IStep<TContext>> steps)
        {
            _steps = steps.ToList();
        }

        public async Task RunAsync(TContext context)
        {
            foreach (var step in _steps)
            {
                if (!step.PreConditionsSatisfied(context))
                {
                    throw new InvalidOperationException($"Step {step.Name} was called but the preconditions were not met");
                }
                await step.ExecuteAsync(context);
            }
        }
    }
}