using System;
using Kekser.ComponentUI;
using Kekser.ComponentUI.Components;

namespace Example.Components
{
    public class Layout: UIComponent
    {
        public override void OnRender(Context ctx, Action<Context> children)
        {
            ctx._<VerticalLayout>(render: ctx =>
            {
                ctx._<Box>();
                ctx._<HorizontalLayout>(render: ctx =>
                {
                    ctx._<Box>();
                    children?.Invoke(ctx);
                });
            });
        }
    }
}