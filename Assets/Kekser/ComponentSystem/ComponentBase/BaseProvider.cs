using System;

namespace Kekser.ComponentSystem.ComponentBase
{
    public abstract class BaseProvider<TNode>: BaseFragment<TNode> where TNode: class, new()
    {
        public override void OnRender(BaseContext<TNode> ctx)
        {
            Children(ctx);
        }
    }
}