namespace Kekser.ComponentSystem.V2.ComponentBase.Core
{
    public interface IFragment
    {
        IFragmentContext GetContext();
        void Init();
        void Dispose();
        void Update();
    }
    
    public interface IFragment<TProps> : IFragment where TProps: class, new()
    {
        TProps Props { get; set; }
    }
}