using System;

namespace Scenes.Kekser.ComponentUI.Components
{
    public class Box : UIComponent
    {
        public override void OnRender(Context ctx, Action<Context> children)
        {
            children?.Invoke(ctx);
        }
    }
}