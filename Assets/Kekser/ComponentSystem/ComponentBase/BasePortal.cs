using Kekser.ComponentSystem.ComponentBase.PropSystem.Rework;
using Kekser.ComponentSystem.ComponentUI;

namespace Kekser.ComponentSystem.ComponentBase
{
    public class BasePortal<TNode>: BaseFragment<TNode, BasePortal<TNode>.PortalProps> where TNode: class, new()
    {
        public struct PortalProps
        {
            public OptionalValue<TNode> target { get; set; }
        }
        
        public override void OnRender(BaseContext<TNode> ctx)
        {
            PortalProps props = Props.Get<PortalProps>();
            if (props.target.IsSet)
                _fragmentNode = props.target;
            else
                _fragmentNode = _fragmentRoot;
            
            Children(ctx);
        }
    }
}