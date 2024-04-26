using System;
using Scenes.Kekser.ComponentUI;
using Scenes.Kekser.ComponentUI.Components;
using UnityEngine;
using Component = Scenes.Kekser.ComponentUI.Component;
using Event = Scenes.Kekser.ComponentUI.Event;

namespace Scenes.Test
{
    public class Menu: Component
    {
        private int _clicks = 0;

        public override void OnMount()
        {
            Props.Set("text", new TextProp() {Value = "Click me!"});
        }

        public override void OnRender(Context ctx, Action<Context> children)
        {
            if (_clicks % 2 == 0) ctx._<Box>();
            ctx._<Box>(render: ctx =>
            {
                ctx._<Button>(
                    props: props => props.Set("onClick", new Event() {Callback = () => Props.Set("text", new TextProp() {Value = "Clicked " + ++_clicks + " times"})}),
                    render: ctx => ctx._<Text>(props: props => props.Set("text", Props.Get<TextProp>("text")))
                );
            });
        }
    }
}