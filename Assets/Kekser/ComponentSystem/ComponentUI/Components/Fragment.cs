using System;
using Kekser.ComponentSystem.ComponentBase;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public sealed class Fragment: UIFragment
    {
        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            Children(ctx);
        }
    }
}