using System;
using System.Reflection;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.PropSystem.Rework;
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
            PropertyInfo[] propertyInfos = typeof(TProps).GetProperties();
            
            foreach (PropertyInfo propertyInfo in propertyInfos)
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
            
            /*try
            {
                if (!Props.Has("style")) return;
                object rawStyle = Props.Get("style");

                Style style;
                switch (rawStyle)
                {
                    case Style styleValue:
                        style = styleValue;
                        break;
                    case IPropValue propValue:
                        if (propValue.IsOptional && !propValue.IsSet)
                            return;
                        if (!propValue.IsOptional && !propValue.IsSet)
                            throw new Exception("Required prop not set");
                        style = (Style)propValue.RawValue;
                        break;
                    default:
                        throw new Exception("Invalid style prop");
                }

                PropertyInfo[] styleProperties = typeof(Style).GetProperties();
                foreach (PropertyInfo styleProperty in styleProperties)
                {
                    PropertyInfo propertyInfo = typeof(IStyle).GetProperty(styleProperty.Name);
                    if (propertyInfo == null) continue;

                    switch (styleProperty.GetValue(style))
                    {
                        case IPropValue propValue:
                            if (propValue.IsOptional && !propValue.IsSet)
                                continue;
                            if (!propValue.IsOptional && !propValue.IsSet)
                                throw new Exception("Required prop not set");
                            propertyInfo.SetValue(FragmentRoot.style, propValue.RawValue);
                            break;
                        default:
                            propertyInfo.SetValue(FragmentRoot.style, styleProperty.GetValue(style));
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                UIRenderer.Log($"Failed to apply style on {FragmentRoot.GetType().Name}");
            }*/
        }
        
        public void ApplyStyle(Style style)
        {
            PropertyInfo[] styleProperties = typeof(Style).GetProperties();
            foreach (PropertyInfo styleProperty in styleProperties)
            {
                PropertyInfo propertyInfo = typeof(IStyle).GetProperty(styleProperty.Name);
                if (propertyInfo == null) continue;

                switch (styleProperty.GetValue(style))
                {
                    case IPropValue propValue:
                        if (propValue.IsOptional && !propValue.IsSet)
                            continue;
                        if (!propValue.IsOptional && !propValue.IsSet)
                            throw new Exception("Required prop not set");
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
            _fragmentRoot = Activator.CreateInstance<TElement>();
            _fragmentRoot.AddToClassList(GetType().Name);
            _fragmentNode = _fragmentRoot;
        }
    }
}