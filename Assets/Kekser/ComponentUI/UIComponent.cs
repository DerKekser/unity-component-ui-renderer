using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Kekser.ComponentUI
{
    public abstract class UIComponent: UIFragment
    {
        private StyleParser _styleParser;

        public override void Mount(Transform parent)
        {
            _node.SetParent(parent);
            _node.localScale = Vector3.one;
            base.Mount(parent);
        }
        
        public override void Unmount()
        {
            _node.SetParent(null);
            base.Unmount();
            Object.Destroy(_node.gameObject);
        }

        public override void Render(Action<Context> children)
        {
            ApplyStyle();
            base.Render(children);
        }

        public void ApplyStyle()
        {
            _styleParser.Parse(Props);
        }

        public override void SetContext(Context ctx)
        {
            base.SetContext(ctx);
            _node = new GameObject(GetType().Name, typeof(RectTransform)).transform as RectTransform;
            _styleParser = new StyleParser(_node);
        }
    }
}