namespace Kekser.ComponentSystem.ComponentBase.PropSystem
{
    public struct Prop: IProp
    {
        private string _key;
        private object _value;

        public Prop(string key, object value)
        {
            _key = key;
            _value = value;
        }

        public void AddToProps(Props props)
        {
            props.Set(_key, _value);
        }
    }
    
    public struct Prop<T>: IProp
    {
        private string _key;
        private T _value;

        public Prop(string key, T value)
        {
            _key = key;
            _value = value;
        }

        public void AddToProps(Props props)
        {
            props.Set(_key, _value);
        }
    }
}