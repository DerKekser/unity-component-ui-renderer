using System;
using Kekser.ComponentUI;
using Kekser.ComponentUI.Extension.ResourceManagement;
using Kekser.ComponentUI.PropSystem;
using UnityEngine;
using UnityEngine.UIElements;
using Box = Kekser.ComponentUI.Components.Box;

namespace Example.Components
{
    public class Layout: UIComponent
    {
        public override void OnRender(Context ctx, Action<Context> children)
        {
            ResourceProvider provider = GetProvider<ResourceProvider>();
            
            ctx._<Box>(render: ctx =>
            {
                ctx._<Box>(
                    props: new IProp[]
                    {
                        new Prop("position", new StyleEnum<Position>(Position.Absolute)),
                        new Prop("width", new StyleLength(500)),
                        new Prop("height", new StyleLength(500)),
                        new Prop("maxWidth", new StyleLength(Length.Percent(90))),
                        new Prop("maxHeight", new StyleLength(Length.Percent(90))),
                        new Prop("top", new StyleLength(Length.Percent(50))),
                        new Prop("left", new StyleLength(Length.Percent(50))),
                        new Prop("translate", new StyleTranslate(new Translate(Length.Percent(-50), Length.Percent(-50), 0))),
                        new Prop("backgroundImage", new StyleBackground(provider.GetResource<Sprite>("Packages/com.unity.collab-proxy/Editor/PlasticSCM/Assets/Images/stepok@2x"))),
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