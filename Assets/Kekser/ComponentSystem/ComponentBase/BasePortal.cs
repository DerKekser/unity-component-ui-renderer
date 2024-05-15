using Kekser.ComponentSystem.ComponentBase.PropSystem;
using UnityEngine;

namespace Kekser.ComponentSystem.ComponentBase
{
    public abstract class PortalProps<TNode> where TNode: class, new()
    {
        public OptionalValue<TNode> target { get; set; } = new();
    }
    
    public abstract class BasePortal<TNode, TPortalProps>: BaseFragment<TNode, TPortalProps> where TNode: class, new() where TPortalProps: PortalProps<TNode>, new()
    {
        private class InternalFragment: BaseFragment<TNode>
        {
            protected override void OnRender()
            {
                Children();
            }
        }

        protected override void OnRender()
        {
            if (Props.target.IsSet)
                _fragmentNode = ((TNode)Props.target) ?? _fragmentRoot;
            else
                _fragmentNode = _fragmentRoot;
            
            _<InternalFragment>(
                key: _fragmentNode.GetHashCode().ToString(),
                render: Children
            );
        }
    }
}