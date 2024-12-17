namespace Kekser.ComponentSystem.V2.ComponentBase
{
    public interface IFragment
    {
        IFragmentContext GetContext();
    }
    
    public interface IFragment<TProps> : IFragment where TProps: class, new()
    {
        TProps Props { get; set; }
    }
}