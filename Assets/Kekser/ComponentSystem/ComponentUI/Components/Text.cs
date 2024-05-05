﻿using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class TextProps: StyleProps
    {
        public OptionalValue<string> text { get; set; } = new();
    }
    
    public sealed class Text: UIComponent<Label, TextProps>
    {
        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            FragmentNode.text = OwnProps.text;
        }
    }
}