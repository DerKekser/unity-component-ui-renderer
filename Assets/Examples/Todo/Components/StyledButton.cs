using System;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.Extension.ResourceManagement;
using UnityEngine;
using UnityEngine.UIElements;
using Button = Kekser.ComponentSystem.ComponentUI.Components.Button;

namespace Examples.Todo.Components
{
    public class StyledButton: UIComponent
    {
        public override void OnRender(BaseContext<VisualElement> ctx, Action<BaseContext<VisualElement>> children)
        {
            ResourceProvider provider = GetProvider<ResourceProvider>();
            
            ctx._<Button>(
                props: new IProp[]
                {
                    new EventProp("onClick", Props.Get<Action>("onClick")),
                    new Prop("width", new StyleLength(Length.Percent(100))),
                    new Prop("height", new StyleLength(Length.Percent(100))),
                    new Prop("backgroundImage", new StyleBackground(provider.GetResource<Sprite>("Resources/unity_builtin_extra/UISprite"))),
                },
                render: ctx => ctx._<Text>(
                    props: new IProp[]
                    {
                        new Prop("text", Props.Get("text")),
                        new Prop("width", new StyleLength(Length.Percent(100))),
                        new Prop("height", new StyleLength(Length.Percent(100))),
                        new Prop("fontSize", new StyleLength(24)),
                        new Prop("color", new StyleColor(Color.red)),
                    }
                )
            );
        }
    }
}