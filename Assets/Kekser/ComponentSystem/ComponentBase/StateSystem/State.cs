using System;

namespace Kekser.ComponentSystem.ComponentBase.StateSystem
{
    public class State<T>
    {
        private T _value;
        private Action _setDirty;
        
        public State(Action setDirty, T defaultValue = default)
        {
            _value = defaultValue;
            _setDirty = setDirty;
        }
        
        public T Value {
            get => _value;
            set {
                _value = value;
                _setDirty();
            }
        }
    }
}