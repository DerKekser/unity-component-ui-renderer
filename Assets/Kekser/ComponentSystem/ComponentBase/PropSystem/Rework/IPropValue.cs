namespace Kekser.ComponentSystem.ComponentBase.PropSystem.Rework
{
    public interface IPropValue
    {
        object RawValue { get; }
        bool IsSet { get; }
        bool IsOptional { get; }
    }
}