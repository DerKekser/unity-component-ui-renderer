using System;

namespace Kekser.ComponentSystem.ComponentBase
{
    public abstract class BaseProvider<TNode, TProps>: BaseFragment<TNode, TProps> where TNode: class, new() where TProps : struct
    {
        public override void OnRender(BaseContext<TNode> ctx)
        {
            Children(ctx);
        }
    }
}