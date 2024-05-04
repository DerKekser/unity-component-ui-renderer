using Kekser.ComponentSystem.ComponentBase.PropSystem.Rework;

namespace Kekser.ComponentSystem.ComponentBase
{
    public interface IFragment<TNode> where TNode: class, new()
    {
        TNode FragmentRoot { get; }
        TNode FragmentNode { get; }
        
        IPropList Props { get; }
        
        void Mount(TNode parent);
        void Unmount();
        void Render();
        void SetContext(BaseContext<TNode> ctx);
        TProvider GetProvider<TProvider>() where TProvider : class, IFragment<TNode>;
    }
}