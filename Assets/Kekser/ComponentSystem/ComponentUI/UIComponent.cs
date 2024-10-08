﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using Kekser.ComponentSystem.StyleGenerator;
using UnityEngine;
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
        private int _contextCreationAttempts = 0;
        
        private static readonly List<(PropertyInfo, PropertyInfo)> StylePropMap = 
            typeof(Style)
                .GetProperties()
                .Select(x => (x, typeof(IStyle).GetProperty(x.Name)))
                .ToList();

        public new TElement FragmentRoot => _fragmentRoot as TElement;

        private void RemoveClasses(VisualElement element)
        {
            element.ClearClassList();
            foreach (VisualElement child in element.Children())
                RemoveClasses(child);
        }
        
        public override void Mount(VisualElement parent)
        {
            RemoveClasses(_fragmentRoot);
            parent?.Add(_fragmentRoot);
            _fragmentNode = _fragmentRoot.contentContainer;
            base.Mount(parent);
        }
        
        public override void Unmount()
        {
            _fragmentRoot?.parent?.Remove(_fragmentRoot);
            base.Unmount();
        }

        public override void Render()
        {
            ApplyStyling();
            base.Render();
        }
        
        public void ApplyStyling()
        {
            if (Props is IStyleProp styleProps && styleProps.style.IsSet)
                ApplyStyle(styleProps.style);
            if (Props is IClassNameProp classNameProps && classNameProps.className.IsSet)
                ApplyClassName(classNameProps.className);
            else 
                ApplyClassName("");
        }
        
        public void ApplyStyle(Style style)
        {
            foreach ((PropertyInfo styleProperty, PropertyInfo iStyleProperty) in StylePropMap)
            {
                if (!styleProperty.CanRead || !iStyleProperty.CanWrite)
                    continue;
                
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

        public void ApplyClassName(string classNames)
        {
            FragmentRoot.ClearClassList();
            foreach (string className in classNames.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                FragmentRoot.AddToClassList(StyleUtils.CleanupClassName(className));
            }
        }

        public override void SetContext(BaseContext<VisualElement> ctx)
        {
            base.SetContext(ctx);
            try
            {
                _fragmentRoot = new TElement();
                _fragmentNode = _fragmentRoot;
            }
            catch (Exception e)
            {
                _contextCreationAttempts++;
                if (_contextCreationAttempts > 5)
                {
                    Debug.LogError($"Error while creating {typeof(TElement).Name} for {GetType().Name}. Aborting.");
                    return;
                }
                Debug.LogError($"Error while creating {typeof(TElement).Name} for {GetType().Name}. Trying again.");
                SetContext(ctx);
            }
        }
    }
}