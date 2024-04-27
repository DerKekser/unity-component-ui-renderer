using System;
using Scenes.Kekser.ComponentUI;
using Scenes.Kekser.ComponentUI.Components;
using UnityEngine;

namespace Scenes.Test
{
    public class Menu: UIComponent
    {
        private int _clicks = 0;

        public override void OnMount()
        {
            Props.Set("text", "Click me!");
        }

        public override void OnRender(Context ctx, Action<Context> children)
        {
            ctx._<VerticalLayout>(render: ctx =>
            {
                if (_clicks % 2 == 0) ctx._<Box>();
                ctx._<Box>(render: ctx =>
                {
                    ctx._<Button>(
                        props: props =>
                            props.Set<Action>("onClick", () => Props.Set("text", "Clicked " + ++_clicks + " times")),
                        render: ctx => ctx._<Text>(props: props =>
                        {
                            props.Set("text", Props.Get("text"));
                            props.Set("fontSize", 24);
                            props.Set("color", Color.red);
                        })
                    );
                });
            });
        }
    }
}