namespace Kekser.ComponentSystem.ComponentBase.PropSystem
{
    public interface IPropList
    {
        public void Set<TProps>(TProps props) where TProps : struct;
        public TProps Get<TProps>() where TProps : struct;
        public bool IsDirty { get; set; }
    }
}