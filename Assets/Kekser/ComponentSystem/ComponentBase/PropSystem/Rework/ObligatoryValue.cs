namespace Kekser.ComponentSystem.ComponentBase.PropSystem.Rework
{
    public struct ObligatoryValue<T>: IPropValue
    {
        private T _value;
        private bool _isSet;

        public bool IsSet => _isSet;
        public bool IsOptional => false;
        
        public bool Equals(IPropValue other)
        {
            if (other is ObligatoryValue<T> obligatoryValue)
                return obligatoryValue._value.Equals(_value);
            return false;
        }

        public void TakeValue(IPropValue value)
        {
            if (value is ObligatoryValue<T> obligatoryValue)
            {
                _isSet = true;
                _value = obligatoryValue._value;
            }
        }
        
        public object ToObject() => _value;

        public static implicit operator T(ObligatoryValue<T> value)
        {
            if (!value._isSet)
                throw new System.Exception("Value is not set");
            return value._value;
        }

        public static implicit operator ObligatoryValue<T>(T value) => new ObligatoryValue<T> { _value = value, _isSet = true };
    }
}