﻿using Kekser.ComponentSystem.ComponentUI.UIProps;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public sealed class Box : UIComponent<UnityEngine.UIElements.Box, StyleProps>
    {
        public override void OnRender()
        {
            Children();
        }
    }
}