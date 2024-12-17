namespace Kekser.ComponentSystem.V2.ComponentBase
{
    public interface IComponent<TNode> : IFragment where TNode: class, new()
    {
        
    }
    
    public interface IComponent<TNode, TProps> : IComponent<TNode>, IFragment<TProps> where TNode: class, new() where TProps: class, new()
    {
        
    }
    
    public interface IComponent<TNode, TObject, TProps> : IComponent<TNode, TProps> where TNode: class, new() where TObject: class, new() where TProps: class, new()
    {
        
    }
}