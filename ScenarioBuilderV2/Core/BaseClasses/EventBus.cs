using ScenarioBuilder.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Core
{
    public sealed class EventBus
    {
        private readonly Dictionary<Type, List<Func<IEvent, Task>>> _handlers = new();

        public void Subscribe<TEvent>(Func<TEvent, Task> handler)
            where TEvent : IEvent
        {
            var type = typeof(TEvent);

            if (!_handlers.ContainsKey(type))
                _handlers[type] = new();

            _handlers[type].Add(e => handler((TEvent)e));
        }

        public async Task PublishAsync(IEvent @event)
        {
            var type = @event.GetType();

            if (!_handlers.TryGetValue(type, out var handlers))
                return;

            foreach (var handler in handlers)
                await handler(@event);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Dictionary<Type, List<Func<IEvent, Task>>> GetHandlers()
        {
            return _handlers;
        }
    }
}