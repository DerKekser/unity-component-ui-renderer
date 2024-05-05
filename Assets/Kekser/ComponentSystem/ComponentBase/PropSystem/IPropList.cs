namespace Kekser.ComponentSystem.ComponentBase.PropSystem
{
    public interface IPropList
    {
        public void Set<TProps>(TProps props) where TProps: class, new();
        public bool IsDirty { get; set; }
    }
    
    public interface IPropList<TProps>: IPropList where TProps: class, new()
    {
        public TProps Props { get; }
        public void Set(TProps props);
        public TProps Get();
    }
}