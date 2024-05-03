namespace Kekser.ComponentSystem.ComponentBase
{
    public class BasePortal<TNode>: BaseFragment<TNode> where TNode: class, new()
    {
        public override void OnRender(BaseContext<TNode> ctx)
        {
            if (Props.Has("target"))
                _fragmentNode = Props.Get<TNode>("target");
            else
                _fragmentNode = _fragmentRoot;
            
            Children(ctx);
        }
    }
}