using System;
using System.Collections.Generic;

namespace TowerDefence.Events
{
    public class StaticEventBus
    {
        private static Dictionary<Type, List<Delegate>> eventSubscribers = new Dictionary<Type, List<Delegate>>();

        public static void Subscribe<T>(Action<T> handler) where T : class
        {
            var eventType = typeof(T);

            if (!eventSubscribers.ContainsKey(eventType))
            {
                eventSubscribers[eventType] = new List<Delegate>();
            }

            eventSubscribers[eventType].Add(handler);
        }

        public static void Unsubscribe<T>(Action<T> handler) where T : class
        {
            var eventType = typeof(T);

            if (eventSubscribers.TryGetValue(eventType, out var handlers))
            {
                handlers.Remove(handler);

                if (handlers.Count == 0)
                {
                    eventSubscribers.Remove(eventType);
                }
            }
        }

        public static void RaiseEvent<T>(T eventArgument) where T : class
        {
            var eventType = typeof(T);

            if (eventSubscribers.ContainsKey(eventType))
            {
                var subscribers = eventSubscribers[eventType];

                foreach (var subscriber in subscribers)
                {
                    var action = subscriber as Action<T>;
                    if (action != null)
                    {
                        action(eventArgument);
                    }
                }
            }
        }
    }
}