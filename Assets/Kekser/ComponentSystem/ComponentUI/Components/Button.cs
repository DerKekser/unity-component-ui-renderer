﻿using System;
using Kekser.ComponentSystem.ComponentBase;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public sealed class Button : UIComponent<UnityEngine.UIElements.Button>
    {
        private void Click()
        {
            Action e = Props.Get<Action>("onClick");
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