using ScenarioBuilderV3.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilderV3.Core
{
    // Domain/ScenarioExecutionOptions.cs
    public sealed class ScenarioExecutionOptions
    {
        private readonly HashSet<Type> _stopBefore = new();
        private readonly Dictionary<Type, IScenarioEvent> _overrides = new();

        public static ScenarioExecutionOptions Default => new();

        public ScenarioExecutionOptions StopBefore<TStep>()
            where TStep : IScenarioEvent
        {
            _stopBefore.Add(typeof(TStep));
            return this;
        }

        public ScenarioExecutionOptions RunUntil<TStep>()
            where TStep : IScenarioEvent
            => StopBefore<TStep>();

        public ScenarioExecutionOptions Override<TStep>(IScenarioEvent replacement)
            where TStep : IScenarioEvent
        {
            _overrides[typeof(TStep)] = replacement;
            return this;
        }

        internal bool ShouldStopBefore(Type stepId) => _stopBefore.Contains(stepId);

        internal bool TryGetOverride(Type stepId, out IScenarioEvent replacement)
            => _overrides.TryGetValue(stepId, out replacement!);
    }
}