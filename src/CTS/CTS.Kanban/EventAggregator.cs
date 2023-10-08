using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTS.Kanban;
internal class EventAggregator : IEventAggregator
{
    private static readonly Dictionary<Type, List<Action<object>>> _subscriptions = new();

    public void Publish<T>(T message)
    {
        var type = typeof(T);
        if(_subscriptions.ContainsKey(type))
        {
            foreach(var subscription in _subscriptions[type])
            {
                subscription(message);
            }
        }
    }

    public void Subscribe<T>(Action<object> action)
    {
        var type = typeof(T);
        if(!_subscriptions.ContainsKey(type))
        {
            _subscriptions.Add(type, new List<Action<object>>());
        }
        _subscriptions[type].Add(action);
    }

    public void Unsubscribe<T>(Action<object> action)
    {
        var type = typeof(T);
        if(_subscriptions.ContainsKey(type))
        {
            _subscriptions[type].Remove(action);
        }
    }

    public void UnsubscribeAll<T>()
    {
        var type = typeof(T);
        if(_subscriptions.ContainsKey(type))
        {
            _subscriptions[type].Clear();
        }
    }
}

internal interface IEventAggregator
{
    void Publish<T>(T message);

    void Subscribe<T>(Action<object> action);

    void Unsubscribe<T>(Action<object> action);

    void UnsubscribeAll<T>();
}