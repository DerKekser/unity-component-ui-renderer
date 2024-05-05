using Kekser.ComponentSystem.ComponentBase.PropSystem;

namespace Kekser.ComponentSystem.ComponentBase
{
    public class BasePortal<TNode>: BaseFragment<TNode, BasePortal<TNode>.PortalProps> where TNode: class, new()
    {
        public class PortalProps
        {
            public OptionalValue<TNode> target { get; set; }
        }
        
        public override void OnRender(BaseContext<TNode> ctx)
        {
            if (OwnProps.target.IsSet)
                _fragmentNode = OwnProps.target;
            else
                _fragmentNode = _fragmentRoot;
            
            Children(ctx);
        }
    }
}