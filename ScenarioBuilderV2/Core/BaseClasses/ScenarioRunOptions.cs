using ScenarioBuilderV2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV2.Core.BaseClasses
{
    public sealed class ScenarioRunOptions<TContext>
    {
        private Type? _runUntilStep;

        public void RunUntil<TStep>()
            where TStep : IStep<TContext>
        {
            _runUntilStep = typeof(TStep);
        }

        public bool ShouldStopBefore(IStep<TContext> step)
            => _runUntilStep != null
               && step.GetType() == _runUntilStep;
    }
}