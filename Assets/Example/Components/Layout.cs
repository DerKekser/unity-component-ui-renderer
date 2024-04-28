using System;
using Kekser.ComponentUI;
using Kekser.ComponentUI.Components;
using Kekser.ComponentUI.PropSystem;
using UnityEngine;

namespace Example.Components
{
    public class Layout: UIComponent
    {
        public override void OnRender(Context ctx, Action<Context> children)
        {
            ctx._<Box>(render: ctx =>
            {
                ctx._<Panel>(
                    props: new IProp[]
                    {
                        new Prop("width", "500px"),
                        new Prop("height", "500px"),
                        new Prop("maxWidth", "90%"),
                        new Prop("maxHeight", "90%"),
                        new Prop("top", "50%"),
                        new Prop("left", "50%"),
                        new Prop("translateX", "-50%"),
                        new Prop("translateY", "-50%"),
                        new Prop("color", Color.red),
                    }
                );
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