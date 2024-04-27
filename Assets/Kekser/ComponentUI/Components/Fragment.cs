using System;

namespace Kekser.ComponentUI.Components
{
    public sealed class Fragment: UIFragment
    {
        public override void OnRender(Context ctx, Action<Context> children)
        {
            children?.Invoke(ctx);
        }
    }
}