using Microsoft.Extensions.DependencyInjection;
using ScenarioBuilder.Core.Interfaces;

namespace ScenarioBuilder.Core
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

        public ScenarioExecutionOptions Override<TStep, TReplacementStep>(IServiceProvider sp)
        where TStep : IScenarioEvent
        where TReplacementStep : IScenarioEvent, new()
        {
            _overrides[typeof(TStep)] = sp.GetRequiredService<TReplacementStep>();
            return this;
        }

        internal bool ShouldStopBefore(Type stepId) => _stopBefore.Contains(stepId);

        internal bool TryGetOverride(Type stepId, out IScenarioEvent replacement)
            => _overrides.TryGetValue(stepId, out replacement!);

        public ScenarioExecutionOptions MergeWith(ScenarioExecutionOptions other)
        {
            var merged = new ScenarioExecutionOptions();

            // copy existing stop-before
            foreach (var s in _stopBefore)
                merged._stopBefore.Add(s);

            // copy existing overrides
            foreach (var kv in _overrides)
                merged._overrides[kv.Key] = kv.Value;

            // append new stop-before values
            foreach (var s in other._stopBefore)
                merged._stopBefore.Add(s);

            // append new overrides (new wins)
            foreach (var kv in other._overrides)
                merged._overrides[kv.Key] = kv.Value;

            return merged;
        }
    }
}