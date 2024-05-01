﻿using System;
using Example.Components;
using Kekser.ComponentUI;
using Kekser.ComponentUI.Components;
using Kekser.ComponentUI.PropSystem;

namespace Example.Pages
{
    public class Options: UIComponent
    {
        private void HandleBack()
        {
            Props.Get<Action>("onBack")?.Invoke();
        }
        
        public override void OnRender(Context ctx, Action<Context> children)
        {
            ctx._<Box>(
                props: new IProp[]
                {
                    new Prop("childControlHeight", false),
                    new Prop("childForceExpandHeight", false),
                    new Prop("spacing", 10),
                },
                render: ctx =>
                {
                    ctx.Each(new int[15], (x, i) => 
                    {
                        ctx._<StyledButton>(
                            key: i.ToString(),
                            props: new IProp[]
                            {
                                new Prop("height", "50px"),
                                new Prop("text", $"Option {i}")
                            }
                        );
                    });
                    ctx._<StyledButton>(
                        props: new IProp[]
                        {
                            new Prop("height", "50px"),
                            new EventProp("onClick", HandleBack),
                            new Prop("text", "Back")
                        }
                    );
                }
            );
        }
    }
}