﻿using System;
using Examples.Todo.Components;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using UnityEngine.UIElements;

namespace Examples.Todo.Pages
{
    public class Options: UIComponent
    {
        private void HandleBack()
        {
            Props.Get<Action>("onBack")?.Invoke();
        }
        
        public override void OnRender(BaseContext<VisualElement> ctx, Action<BaseContext<VisualElement>> children)
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