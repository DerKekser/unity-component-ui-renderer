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
        public new TElement Node => _node as TElement;

        public override void Mount(VisualElement parent)
        {
            parent?.Add(_node);
            base.Mount(parent);
        }
        
        public override void Unmount()
        {
            _node?.parent?.Remove(_node);
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
                    typeof(IStyle).GetProperty(styleProperty.Name)?.SetValue(Node.style, Props.Get(styleProperty.Name));
                }
                catch (Exception e)
                {
                    UIRenderer.Log($"Failed to set style property {styleProperty.Name} on {Node.GetType().Name} with value {Props.Get(styleProperty.Name)}");
                }
            }
        }

        public override void SetContext(BaseContext<VisualElement> ctx)
        {
            base.SetContext(ctx);
            _node = Activator.CreateInstance<TElement>();
            _node.AddToClassList(GetType().Name);
        }
    }
}