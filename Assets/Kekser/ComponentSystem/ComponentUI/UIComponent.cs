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
    
    public abstract class UIComponent<TProps> : UIComponent<VisualElement, TProps> where TProps : class, new()
    {
        
    }
    
    public abstract class UIComponent<TElement, TProps>: UIFragment<TProps> where TElement: VisualElement, new() where TProps : class, new()
    {
        private static readonly PropertyInfo StylePropProperty = 
            typeof(TProps)
                .GetProperty("style");
        private static readonly List<(PropertyInfo, PropertyInfo)> StylePropMap = 
            typeof(Style)
                .GetProperties()
                .Select(x => (x, typeof(IStyle).GetProperty(x.Name)))
                .ToList();

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
            switch (StylePropProperty.GetValue(OwnProps))
            {
                case OptionalValue<Style> optionalValue:
                    if (optionalValue.IsSet)
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
        
        public void ApplyStyle(Style style)
        {
            foreach ((PropertyInfo styleProperty, PropertyInfo iStyleProperty) in StylePropMap)
            {
                object styleValue = styleProperty.GetValue(style);
                switch (styleValue)
                {
                    case IPropValue propValue:
                        if (!propValue.IsSet)
                            continue;
                        iStyleProperty.SetValue(FragmentRoot.style, propValue.ToObject());
                        break;
                    default:
                        iStyleProperty.SetValue(FragmentRoot.style, styleValue);
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