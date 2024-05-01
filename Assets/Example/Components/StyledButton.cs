using System;
using Example.Providers;
using Kekser.ComponentUI;
using Kekser.ComponentUI.Components;
using Kekser.ComponentUI.Extension.ResourceManagement;
using Kekser.ComponentUI.PropSystem;
using UnityEngine;

namespace Example.Components
{
    public class StyledButton: UIComponent
    {
        public override void OnRender(Context ctx, Action<Context> children)
        {
            ResourceProvider provider = GetProvider<ResourceProvider>();
            
            ctx._<Button>(
                props:new IProp[]
                {
                    new Prop("sprite", provider.GetResource<Sprite>("Resources/unity_builtin_extra/UISprite")),
                    new Prop("spriteType", UnityEngine.UI.Image.Type.Tiled),
                    new EventProp("onClick", Props.Get<Action>("onClick")),
                },
                render: ctx => ctx._<Text>(
                    props: new IProp[]
                    {
                        new Prop("text", Props.Get("text")),
                        new Prop("fontSize", 24),
                        new Prop("color", Color.red)
                    }
                )
            );
        }
    }
}