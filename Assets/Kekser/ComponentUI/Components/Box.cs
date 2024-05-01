using System;
using UnityEngine.UIElements;

namespace Kekser.ComponentUI.Components
{
    public sealed class Box : UIComponent<VisualElement>
    {
        public override void OnRender(Context ctx, Action<Context> children)
        {
            children?.Invoke(ctx);
        }
    }
}