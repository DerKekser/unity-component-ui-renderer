namespace Kekser.ComponentSystem.ComponentBase.PropSystem.Rework
{
    public struct OptionalValue<T>: IPropValue
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
        public bool IsOptional => true;
        
        public static implicit operator T(OptionalValue<T> value) => value._value;
        public static implicit operator OptionalValue<T>(T value) => new OptionalValue<T> { _value = value, _isSet = true };
    }
}