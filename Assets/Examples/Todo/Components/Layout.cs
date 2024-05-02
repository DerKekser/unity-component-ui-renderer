using System;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.Extension.ResourceManagement;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using UnityEngine;
using UnityEngine.UIElements;
using Box = Kekser.ComponentSystem.ComponentUI.Components.Box;

namespace Examples.Todo.Components
{
    public class Layout: UIComponent
    {
        public override void OnRender(BaseContext<VisualElement> ctx, Action<BaseContext<VisualElement>> children)
        {
            ResourceProvider<VisualElement> provider = GetProvider<ResourceProvider<VisualElement>>();
            
            ctx._<Box>(
                props: new IProp[]
                {
                    new Prop("position", new StyleEnum<Position>(Position.Absolute)),
                    new Prop("width", new StyleLength(500)),
                    new Prop("height", new StyleLength(500)),
                    new Prop("paddingBottom", new StyleLength(20)),
                    new Prop("paddingLeft", new StyleLength(20)),
                    new Prop("paddingRight", new StyleLength(20)),
                    new Prop("paddingTop", new StyleLength(20)),
                    new Prop("maxWidth", new StyleLength(Length.Percent(90))),
                    new Prop("maxHeight", new StyleLength(Length.Percent(90))),
                    new Prop("top", new StyleLength(Length.Percent(50))),
                    new Prop("left", new StyleLength(Length.Percent(50))),
                    new Prop("translate", new StyleTranslate(new Translate(Length.Percent(-50), Length.Percent(-50), 0))),
                    new Prop("backgroundImage", new StyleBackground(provider.GetResource<Sprite>("Kenny UI/Spritesheet/greySheet/grey_panel.png"))),
                    new Prop("unityFont", new StyleFont(provider.GetResource<Font>("Kenny UI/Font/kenvector_future"))),
                },
                render: ctx => children?.Invoke(ctx)
            );
        }
    }
}