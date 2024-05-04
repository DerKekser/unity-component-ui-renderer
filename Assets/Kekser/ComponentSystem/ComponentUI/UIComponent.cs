using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI
{
    public abstract class UIComponent : UIComponent<StyleProps>
    {
        
    }
    
    public abstract class UIComponent<TProps> : UIComponent<VisualElement, TProps> where TProps : struct
    {
        
    }
    
    public abstract class UIComponent<TElement, TProps>: UIFragment<TProps> where TElement: VisualElement, new() where TProps : struct
    {
        private static readonly PropertyInfo[] StyleProperties = typeof(Style).GetProperties();
        private static readonly PropertyInfo[] PropProperties = typeof(TProps).GetProperties();
        private static readonly Dictionary<string, PropertyInfo> IStyleProperties = typeof(IStyle).GetProperties().ToDictionary(x => x.Name);

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

        public override void Render()
        {
            ApplyStyle();
            base.Render();
        }
        
        public void ApplyStyle()
        {
            foreach (PropertyInfo propertyInfo in PropProperties)
            {
                switch (propertyInfo.GetValue(OwnProps))
                {
                    case OptionalValue<Style> optionalValue:
                        if (!optionalValue.IsSet)
                            continue;
                        ApplyStyle(optionalValue);
                        break;
                    case ObligatoryValue<Style> obligatoryValue:
                        if (!obligatoryValue.IsSet)
                            throw new Exception("Required prop not set");
                        ApplyStyle(obligatoryValue);
                        break;
                    case Style style:
                        ApplyStyle(style);
                        break;
                    default:
                        break;
                }
            }
        }
        
        public void ApplyStyle(Style style)
        {
            foreach (PropertyInfo styleProperty in StyleProperties)
            {
                if (!IStyleProperties.TryGetValue(styleProperty.Name, out PropertyInfo propertyInfo))
                    continue;
                switch (styleProperty.GetValue(style))
                {
                    case IPropValue propValue:
                        if (!propValue.IsSet)
                            continue;
                        propertyInfo.SetValue(FragmentRoot.style, propValue.ToObject());
                        break;
                    default:
                        propertyInfo.SetValue(FragmentRoot.style, styleProperty.GetValue(style));
                        break;
                }
            }
        }

        public override void SetContext(BaseContext<VisualElement> ctx)
        {
            base.SetContext(ctx);
            _fragmentRoot = new TElement();
            _fragmentRoot.AddToClassList(GetType().Name);
            _fragmentNode = _fragmentRoot;
        }
    }
}