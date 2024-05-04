﻿using System;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.PropSystem.Rework;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public struct ButtonProps
    {
        public OptionalValue<Action> onClick { get; set; }
        public OptionalValue<Style> style { get; set; }
    }
    
    public sealed class Button : UIComponent<UnityEngine.UIElements.Button, ButtonProps>
    {
        private void Click()
        {
            Action e = Props.Get<ButtonProps>().onClick;
            e?.Invoke();
        }
        
        public override void OnMount()
        {
            FragmentNode.clickable.clicked += Click;
            FragmentNode.text = "";
        }
        
        public override void OnUnmount()
        {
            FragmentNode.clickable.clicked -= Click;
        }
        
        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            Children(ctx);
        }
    }
}