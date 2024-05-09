namespace Kekser.ComponentSystem.ComponentBase.PropSystem
{
    public class OptionalValue<T>: IPropValue
    {
        private T _value;
        private bool _isSet;

        public bool IsSet => _isSet;
        public bool IsOptional => true;
        
        public OptionalValue() { }
        public OptionalValue(T value)
        {
            _value = value;
            _isSet = true;
        }
        
        public bool Equals(IPropValue other)
        {
            if (other is OptionalValue<T> obligatoryValue)
            {
                if (obligatoryValue._value == null) return _value == null;
                return obligatoryValue._value.Equals(_value);
            }
            return false;
        }

        public void TakeValue(IPropValue value)
        {
            if (value is OptionalValue<T> obligatoryValue)
            {
                _isSet = true;
                _value = obligatoryValue._value;
            }
        }
        
        public object ToObject() => _value;
        
        public static implicit operator T(OptionalValue<T> value) => value._value;
        public static implicit operator OptionalValue<T>(T value) => new OptionalValue<T> { _value = value, _isSet = true };
    }
}