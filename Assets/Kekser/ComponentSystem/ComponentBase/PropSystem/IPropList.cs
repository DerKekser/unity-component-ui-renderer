namespace Kekser.ComponentSystem.ComponentBase.PropSystem
{
    public interface IPropList
    {
        public void Set<TProps>(TProps props) where TProps : class;
        public TProps Get<TProps>() where TProps : class;
        public bool IsDirty { get; set; }
    }
}