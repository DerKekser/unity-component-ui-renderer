using System;
using Scenes.Kekser.ComponentUI;
using Scenes.Kekser.ComponentUI.Components;
using UnityEngine;

namespace Scenes.Test.Components
{
    public class MenuButton: UIComponent
    {
        public override void OnRender(Context ctx, Action<Context> children)
        {
            ctx._<Button>(
                props: props =>
                    props.Set<Action>("onClick", Props.Get<Action>("onClick")),
                render: ctx => ctx._<Text>(props: props =>
                {
                    props.Set("text", Props.Get("text"));
                    props.Set("fontSize", 24);
                    props.Set("color", Color.red);
                })
            );
        }
    }
}