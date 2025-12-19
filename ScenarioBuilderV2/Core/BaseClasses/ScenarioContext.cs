using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Core
{
    public sealed class ScenarioContext
    {
        public EventBus Events { get; } = new();

        private readonly Dictionary<Type, object> _data = new();

        public void Set<T>(T value)
            => _data[typeof(T)] = value;

        public T Get<T>()
            => (T)_data[typeof(T)];

        public bool TryGet<T>(out T value)
        {
            if (_data.TryGetValue(typeof(T), out var obj))
            {
                value = (T)obj;
                return true;
            }

            value = default!;
            return false;
        }
    }
}