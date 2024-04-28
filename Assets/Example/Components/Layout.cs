using System;
using Kekser.ComponentUI;
using Kekser.ComponentUI.Components;
using Kekser.ComponentUI.PropSystem;

namespace Example.Components
{
    public class Layout: UIComponent
    {
        public override void OnRender(Context ctx, Action<Context> children)
        {
            ctx._<Box>(render: ctx =>
            {
                ctx._<Box>(
                    props: new IProp[]
                    {
                        new Prop("top", "20%"),
                        new Prop("left", "40%"),
                        new Prop("bottom", "5%"),
                        new Prop("right", "5%"),
                    },
                    render: ctx =>
                    {
                        children?.Invoke(ctx);
                    }
                );
            });
        }
    }
}