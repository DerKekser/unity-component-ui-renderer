using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.PropSystem.Rework;

namespace Kekser.ComponentSystem.ComponentBase
{
    public struct NoProps {}
    
    public abstract class BaseFragment<TNode> : BaseFragment<TNode, NoProps> where TNode : class, new() {}
    
    public abstract class BaseFragment<TNode, TProps> : IFragment<TNode> where TNode : class, new() where TProps : struct
    {
        protected TNode _fragmentRoot;
        protected TNode _fragmentNode;
        protected BaseContext<TNode> _ctx;
        
        public TNode FragmentRoot => _fragmentRoot;
        public TNode FragmentNode => _fragmentNode ?? _fragmentRoot;
        public IPropList Props => _ctx?.PropList;
        
        //public virtual IProp[] DefaultProps => null;
        
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

        public virtual void Render()
        {
            BaseRenderer<TNode>.Log("Rendering " + GetType().Name);
            OnRender(_ctx);
        }
        
        public virtual void SetContext(BaseContext<TNode> ctx)
        {
            _ctx = ctx;
            _fragmentRoot = _ctx?.Parent?.Fragment?.FragmentRoot;
            _fragmentNode = _ctx?.Parent?.Fragment?.FragmentNode;
        }
        
        public TProvider GetProvider<TProvider>() where TProvider : class, IFragment<TNode>
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

        protected void Children(BaseContext<TNode> ctx)
        {
            _ctx.Render(ctx);
        }

        public virtual void OnMount() {}
        public virtual void OnUnmount() {}
        public virtual void OnRender(BaseContext<TNode> ctx) {}
    }
}