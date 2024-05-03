namespace Kekser.ComponentSystem.ComponentBase.PropSystem.Rework
{
    public struct ObligatoryValue<T>: IPropValue
    {
        private T _value;
        private bool _isSet;

        public object RawValue => _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                _isSet = true;
            }
        }
        
        public bool IsSet => _isSet;
        public bool IsOptional => false;
        
        public static implicit operator T(ObligatoryValue<T> value) => value._value;
        public static implicit operator ObligatoryValue<T>(T value) => new ObligatoryValue<T> { _value = value, _isSet = true };
    }
}