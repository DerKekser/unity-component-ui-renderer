using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;

namespace Kekser.ComponentSystem.ComponentBase
{
    public abstract class BaseFragment<TNode> where TNode: class, new()
    {
        protected TNode _node;
        protected BaseContext<TNode> _ctx;
        
        public TNode Node => _node;
        public Props Props => _ctx?.Props ?? new Props();
        
        public virtual void Mount(TNode parent)
        {
            BaseRenderer<TNode>.Log("Mounting " + GetType().Name);
            OnMount();
        }
        
        public virtual void Unmount()
        {
            BaseRenderer<TNode>.Log("Unmounting " + GetType().Name);
            OnUnmount();
        }

        public virtual void Render(Action<BaseContext<TNode>> children)
        {
            BaseRenderer<TNode>.Log("Rendering " + GetType().Name);
            OnRender(_ctx, children);
        }
        
        public virtual void SetContext(BaseContext<TNode> ctx)
        {
            _ctx = ctx;
            _node = _ctx?.Parent?.Fragment?.Node;
        }
        
        public TProvider GetProvider<TProvider>() where TProvider : BaseProvider<TNode>
        {
            if (this is TProvider provider)
            {
                return provider;
            }
            
            provider = _ctx?.Parent?.Fragment?.GetProvider<TProvider>();
            if (provider != null)
            {
                return provider;
            }
            
            throw new Exception($"Provider {typeof(TProvider).Name} not found");
        }

        public virtual void OnMount() {}
        public virtual void OnUnmount() {}
        public virtual void OnRender(BaseContext<TNode> ctx, Action<BaseContext<TNode>> children) {}
    }
}