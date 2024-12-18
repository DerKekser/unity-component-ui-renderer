namespace Kekser.ComponentSystem.V2.ComponentBase.Core
{
    public sealed class NoProps
    {
        
    }
    
    public abstract class BaseComponent<TNode, TProps> : BaseFragment<TProps>, IComponent<TNode, TProps> where TNode: class, new() where TProps: class, new()
    {
        
    }
    
    public abstract class BaseComponent<TNode> : BaseComponent<TNode, NoProps> where TNode: class, new()
    {
        
    }

    public abstract class BaseComponent<TNode, TObject, TProps> : BaseComponent<TNode, TProps>, IComponent<TNode, TObject, TProps> where TNode: class, new() where TObject: class, TNode, new() where TProps: class, new()
    {
        
    }
}