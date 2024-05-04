﻿using Kekser.ComponentSystem.ComponentBase;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI
{
    public sealed class UIRenderer: BaseRenderer<VisualElement>
    {
        public override BaseContext<VisualElement> CreateContext(VisualElement rootNode)
        {
            return new UIContext(rootNode);
        }

        protected override void Tick(BaseContext<VisualElement> ctx)
        {
            /*ctx.PropList.Set("screenWidth", Screen.width);
            ctx.PropList.Set("screenHeight", Screen.height);*/
        }
    }
}