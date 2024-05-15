namespace Kekser.ComponentSystem.ComponentBase
{
    public abstract class BaseProvider<TNode, TProps>: BaseFragment<TNode, TProps> where TNode: class, new() where TProps : class, new()
    {
        protected override void OnRender()
        {
            Children();
        }
    }
}