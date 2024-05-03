﻿using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;

namespace Kekser.ComponentSystem.ComponentBase
{
    public abstract class BaseFragment<TNode> where TNode: class, new()
    {
        protected TNode _fragmentRoot;
        protected TNode _fragmentNode;
        protected BaseContext<TNode> _ctx;
        
        public TNode FragmentRoot => _fragmentRoot;
        public TNode FragmentNode => _fragmentNode ?? _fragmentRoot;
        public Props Props => _ctx?.Props ?? new Props();
        
        public virtual IProp[] DefaultProps => null;
        
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

        protected void Children(BaseContext<TNode> ctx)
        {
            _ctx.Render(ctx);
        }

        public virtual void OnMount() {}
        public virtual void OnUnmount() {}
        public virtual void OnRender(BaseContext<TNode> ctx) {}
    }
}