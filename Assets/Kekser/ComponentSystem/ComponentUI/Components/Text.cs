﻿using System;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.PropSystem.Rework;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public struct TextProps
    {
        public OptionalValue<string> text { get; set; }
        public OptionalValue<Style> style { get; set; }
    }
    
    public sealed class Text: UIComponent<Label>
    {
        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            TextProps props = Props.Get<TextProps>();
            FragmentNode.text = Props.Get<TextProps>().text;
        }
    }
}