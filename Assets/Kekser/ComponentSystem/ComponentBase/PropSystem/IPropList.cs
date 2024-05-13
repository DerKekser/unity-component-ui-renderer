namespace Kekser.ComponentSystem.ComponentBase.PropSystem
{
    public interface IPropList<TProps> where TProps: class, new()
    {
        public TProps Props { get; }
        public void Set(TProps props);
        public TProps Get();
    }
}