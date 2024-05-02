using System;
using System.Reflection;
using Kekser.ComponentSystem.ComponentBase;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI
{
    public abstract class UIComponent : UIComponent<VisualElement>
    {
        
    }
    
    public abstract class UIComponent<TElement>: UIFragment where TElement: VisualElement, new()
    {
        public new TElement FragmentRoot => _fragmentRoot as TElement;
        public new TElement FragmentNode => _fragmentNode as TElement;

        public override void Mount(VisualElement parent)
        {
            parent?.Add(_fragmentRoot);
            base.Mount(parent);
        }
        
        public override void Unmount()
        {
            _fragmentRoot?.parent?.Remove(_fragmentRoot);
            base.Unmount();
        }

        public override void Render(Action<BaseContext<VisualElement>> children)
        {
            ApplyStyle();
            base.Render(children);
        }

        public void ApplyStyle()
        {
            PropertyInfo[] styleProperties = typeof(IStyle).GetProperties();
            foreach (PropertyInfo styleProperty in styleProperties) {
                if (!Props.Has(styleProperty.Name)) continue;
                try
                {
                    styleProperty.SetValue(FragmentRoot.style, Props.Get(styleProperty.Name));
                }
                catch (Exception e)
                {
                    UIRenderer.Log($"Failed to set style property {styleProperty.Name} on {FragmentRoot.GetType().Name} with value {Props.Get(styleProperty.Name)}");
                }
            }
        }

        public override void SetContext(BaseContext<VisualElement> ctx)
        {
            base.SetContext(ctx);
            _fragmentRoot = Activator.CreateInstance<TElement>();
            _fragmentRoot.AddToClassList(GetType().Name);
            _fragmentNode = _fragmentRoot;
        }
    }
}