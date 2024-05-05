using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;

namespace Kekser.ComponentSystem.ComponentBase
{
    public class NoProps {}
    
    public abstract class BaseFragment<TNode> : BaseFragment<TNode, NoProps> where TNode : class, new() {}
    
    public abstract class BaseFragment<TNode, TProps> : IFragment<TNode, TProps> where TNode : class, new() where TProps : class, new()
    {
        protected TNode _fragmentRoot;
        protected TNode _fragmentNode;
        protected BaseContext<TNode> _ctx;
        protected PropList<TProps> _props = new PropList<TProps>();
        
        public TNode FragmentRoot => _fragmentRoot;
        public TNode FragmentNode => _fragmentNode ?? _fragmentRoot;
        public IPropList Props => _props;
        
        public TProps OwnProps => _props.Props;
        public virtual TProps DefaultProps { get; } = new TProps();
        
        public virtual void Mount(TNode parent)
        {
            BaseRenderer<TNode>.Log(() => "Mounting " + GetType().Name);
            OnMount();
        }
        
        public virtual void Unmount()
        {
            BaseRenderer<TNode>.Log(() => "Unmounting " + GetType().Name);
            OnUnmount();
        }

        public virtual void Render()
        {
            BaseRenderer<TNode>.Log(() => "Rendering " + GetType().Name);
            OnRender(_ctx);
        }
        
        public virtual void SetContext(BaseContext<TNode> ctx)
        {
            _ctx = ctx;
            _fragmentRoot = _ctx?.Parent?.Fragment?.FragmentRoot;
            _fragmentNode = _ctx?.Parent?.Fragment?.FragmentNode;
            Props.Set(DefaultProps);
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