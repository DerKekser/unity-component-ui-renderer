using System;

namespace Kekser.ComponentSystem.ComponentBase.PropSystem
{
    public struct EventProp: IProp
    {
        private string _key;
        private Action _action;

        public EventProp(string key, Action action)
        {
            _key = key;
            _action = action;
        }

        public void AddToProps(Props props)
        {
            props.Set(_key, _action);
        }
    }
    
    public struct EventProp<T>: IProp
    {
        private string _key;
        private Action<T> _action;

        public EventProp(string key, Action<T> action)
        {
            _key = key;
            _action = action;
        }

        public void AddToProps(Props props)
        {
            props.Set(_key, _action);
        }
    }
}