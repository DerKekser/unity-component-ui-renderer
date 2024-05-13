using System;

namespace Kekser.ComponentSystem.ComponentBase.StateSystem
{
    public class BaseState<T>
    {
        private T _value;
        private Action _setDirty;
        
        public BaseState(Action setDirty, T defaultValue = default)
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