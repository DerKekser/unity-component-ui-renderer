﻿using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI
{
    public class UIRendererProps
    {
        public OptionalValue<int> screenWidth { get; set; } = new();
        public OptionalValue<int> screenHeight { get; set; } = new();
    }
    
    public sealed class UIRenderer: BaseRenderer<VisualElement>
    {
        private RenderFragment<VisualElement, UIRendererProps> _fragment;
        
        public override BaseContext<VisualElement> CreateContext(VisualElement rootNode)
        {
            for (int i = rootNode.childCount - 1; i >= 0; i--)
                rootNode.RemoveAt(i);
            
            _fragment = new RenderFragment<VisualElement, UIRendererProps>(rootNode);
            return new UIContext(_fragment);
        }

        protected override void Tick(BaseContext<VisualElement> ctx)
        {
            _fragment.Props = new UIRendererProps
            {
                screenWidth = Screen.width,
                screenHeight = Screen.height
            };
        }
    }
}