﻿using System;
using Kekser.ComponentSystem.ComponentBase;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public sealed class Text: UIComponent<Label>
    {
        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            FragmentNode.text = Props.Get("text", "");
        }
    }
}