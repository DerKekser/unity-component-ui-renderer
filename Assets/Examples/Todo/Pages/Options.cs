﻿using System;
using Examples.Todo.Components;
using Kekser.ComponentUI;
using Kekser.ComponentUI.Components;
using Kekser.ComponentUI.PropSystem;
using UnityEngine.UIElements;

namespace Examples.Todo.Pages
{
    public class Options: UIComponent
    {
        private void HandleBack()
        {
            Props.Get<Action>("onBack")?.Invoke();
        }
        
        public override void OnRender(Context ctx, Action<Context> children)
        {
            ctx._<ScrollArea>(
                render: ctx =>
                {
                    ctx.Each(new int[15], (x, i) => 
                    {
                        ctx._<StyledButton>(
                            key: i.ToString(),
                            props: new IProp[]
                            {
                                new Prop("height", new StyleLength(50)),
                                new Prop("flexShrink", new StyleFloat(0f)),
                                new Prop("text", $"Option {i}")
                            }
                        );
                    });
                }
            );
            ctx._<StyledButton>(
                props: new IProp[]
                {
                    new Prop("height", new StyleLength(50)),
                    new Prop("flexShrink", new StyleFloat(0f)),
                    new EventProp("onClick", HandleBack),
                    new Prop("text", "Back")
                }
            );
        }
    }
}