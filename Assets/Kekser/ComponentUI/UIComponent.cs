using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Kekser.ComponentUI
{
    public abstract class UIComponent
    {
        private RectTransform _node;
        private Context _ctx;
        
        private StyleParser _styleParser;
        
        public RectTransform Node => _node;
        public Props Props => _ctx?.Props ?? new Props();
        
        public UIComponent()
        {
            // TODO: Add support for fragments
            _node = new GameObject(GetType().Name, typeof(RectTransform)).transform as RectTransform;
            _styleParser = new StyleParser(_node);
        }
        
        public void Mount(Transform parent)
        {
            UIRenderer.Log("Mounting " + GetType().Name);
            _node.SetParent(parent);
            _node.localScale = Vector3.one;
            OnMount();
        }
        
        public void Unmount()
        {
            UIRenderer.Log("Unmounting " + GetType().Name);
            OnUnmount();
            Object.Destroy(_node.gameObject);
        }

        public void Render(Action<Context> children)
        {
            UIRenderer.Log("Rendering " + GetType().Name);
            ApplyStyle(/*Props.Get<Style>("style")*/);
            OnRender(_ctx, children);
        }
        
        public void ApplyStyle()
        {
            _styleParser.Parse();
        }
        
        public void SetContext(Context ctx)
        {
            _ctx = ctx;
        }

        public virtual void OnMount() {}
        public virtual void OnUnmount() {}
        public virtual void OnRender(Context ctx, Action<Context> children) {}
    }
}