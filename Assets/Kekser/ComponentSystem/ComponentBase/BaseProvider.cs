namespace Kekser.ComponentSystem.ComponentBase
{
    public abstract class BaseProvider<TNode, TProps>: BaseFragment<TNode, TProps> where TNode: class, new() where TProps : class, new()
    {
        public override void OnRender()
        {
            Children();
        }
    }
}