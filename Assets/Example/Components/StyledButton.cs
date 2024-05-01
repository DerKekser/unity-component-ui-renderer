using System;
using Example.Providers;
using Kekser.ComponentUI;
using Kekser.ComponentUI.Components;
using Kekser.ComponentUI.Extension.ResourceManagement;
using Kekser.ComponentUI.PropSystem;
using UnityEngine;
using UnityEngine.UIElements;
using Box = Kekser.ComponentUI.Components.Box;
using Button = Kekser.ComponentUI.Components.Button;

namespace Example.Components
{
    public class StyledButton: UIComponent
    {
        public override void OnRender(Context ctx, Action<Context> children)
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