namespace Kekser.ComponentSystem.ComponentBase.PropSystem.Rework
{
    public interface IPropValue
    {
        bool IsSet { get; }
        bool IsOptional { get; }
        bool Equals(IPropValue other);
        void TakeValue(IPropValue value);
        
        public object ToObject();
    }
}