using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Core
{
    public sealed class ScenarioBuilder<TContext>
    {
        private readonly List<IStep<TContext>> _steps = new();

        private ScenarioBuilder()
        { }

        public static ScenarioBuilder<TContext> Create()
            => new();

        public ScenarioBuilder<TContext> Given(IStep<TContext> step)
        {
            _steps.Add(step);
            return this;
        }

        public ScenarioBuilder<TContext> When(IStep<TContext> step)
            => Given(step);

        public ScenarioBuilder<TContext> Then(IStep<TContext> step)
            => Given(step);

        public ScenarioBuilder<TContext> And(IStep<TContext> step)
            => Given(step);

        public Scenario<TContext> Build()
            => new Scenario<TContext>(_steps);
    }
}