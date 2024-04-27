using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Kekser.ComponentUI
{
    public abstract class UIFragment
    {
        protected RectTransform _node;
        protected Context _ctx;
        
        public RectTransform Node => _node;
        public Props Props => _ctx?.Props ?? new Props();
        
        public virtual void Mount(Transform parent)
        {
            UIRenderer.Log("Mounting " + GetType().Name);
            OnMount();
        }
        
        public virtual void Unmount()
        {
            UIRenderer.Log("Unmounting " + GetType().Name);
            OnUnmount();
        }

        public virtual void Render(Action<Context> children)
        {
            UIRenderer.Log("Rendering " + GetType().Name);
            OnRender(_ctx, children);
        }
        
        public virtual void SetContext(Context ctx)
        {
            _ctx = ctx;
            _node = _ctx?.Parent?.UIFragment?.Node;
        }
        
        public TProvider GetProvider<TProvider>() where TProvider : UIProvider
        {
            if (this is TProvider provider)
            {
                return provider;
            }
            
            provider = _ctx?.Parent?.UIFragment?.GetProvider<TProvider>();
            if (provider != null)
            {
                return provider;
            }
            
            throw new Exception($"Provider {typeof(TProvider).Name} not found");
        }

        public virtual void OnMount() {}
        public virtual void OnUnmount() {}
        public virtual void OnRender(Context ctx, Action<Context> children) {}
    }
}