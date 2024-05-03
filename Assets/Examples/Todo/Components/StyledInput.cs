using System;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.Extension.ResourceManagement;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using UnityEngine;
using UnityEngine.UIElements;
using Input = Kekser.ComponentSystem.ComponentUI.Components.Input;

namespace Examples.Todo.Components
{
    public class StyledInput: UIComponent
    {
        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            ResourceProvider<VisualElement> provider = GetProvider<ResourceProvider<VisualElement>>();
            
            ctx._<Input>(
                props: new IProp[]
                {
                    new EventProp<string>("onChange", Props.Get<Action<string>>("onChange")),
                    new Prop("value", Props.Get<string>("value")),
                    new Prop("width", new StyleLength(Length.Percent(100))),
                    new Prop("height", new StyleLength(Length.Percent(100))),
                    new Prop("paddingLeft", new StyleLength(10)),
                    new Prop("paddingRight", new StyleLength(10)),
                    new Prop("fontSize", new StyleLength(24)),
                    new Prop("color", new StyleColor(Color.black)),
                    new Prop("backgroundImage", new StyleBackground(provider.GetResource<Sprite>("Kenny UI/Spritesheet/greySheet/grey_button13.png"))),
                }
            );
        }
    }
}