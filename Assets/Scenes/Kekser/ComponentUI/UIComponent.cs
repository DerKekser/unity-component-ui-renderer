using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scenes.Kekser.ComponentUI
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
            _node = new GameObject(GetType().Name, typeof(RectTransform)).transform as RectTransform;
            _styleParser = new StyleParser(_node);
        }
        
        public void Mount(RectTransform parent)
        {
            Debug.Log("Mounting " + GetType().Name);
            _node.SetParent(parent);
            OnMount();
        }
        
        public void Unmount()
        {
            Debug.Log("Unmounting " + GetType().Name);
            OnUnmount();
            Object.Destroy(_node.gameObject);
        }

        public void Render(Action<Context> children)
        {
            Debug.Log("Rendering " + GetType().Name);
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