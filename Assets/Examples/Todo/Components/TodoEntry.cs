using System;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.Extension.ResourceManagement;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using UnityEngine;
using UnityEngine.UIElements;
using Box = Kekser.ComponentSystem.ComponentUI.Components.Box;
using Button = Kekser.ComponentSystem.ComponentUI.Components.Button;

namespace Examples.Todo.Components
{
    public class TodoEntry: UIComponent
    {
        private void HandleRemove()
        {
            Props.Get<Action>("onRemove")?.Invoke();
        }
        
        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            string todo = Props.Get<string>("todo");
            ResourceProvider<VisualElement> resProvider = GetProvider<ResourceProvider<VisualElement>>();
            
            ctx._<Box>(
                props: new IProp[]
                {
                    new Prop("height", new StyleLength(50)),
                    new Prop("flexShrink", new StyleFloat(0f)),
                    new Prop("flexDirection", new StyleEnum<FlexDirection>(FlexDirection.Row)),
                    new Prop("justifyContent", new StyleEnum<Justify>(Justify.SpaceBetween)),
                    new Prop("alignItems", new StyleEnum<Align>(Align.Center)),
                },
                render: ctx =>
                {
                    ctx._<Text>(
                        props: new IProp[]
                        {
                            new Prop("text", todo),
                            new Prop("fontSize", new StyleLength(20)),
                            new Prop("flexGrow", new StyleFloat(1)),
                        }
                    );
                    ctx._<Button>(
                        props: new IProp[]
                        {
                            new EventProp("onClick", HandleRemove),
                            new Prop("width", new StyleLength(30)),
                            new Prop("height", new StyleLength(30)),
                            new Prop("backgroundImage", new StyleBackground(resProvider.GetResource<Sprite>("Kenny UI/Spritesheet/blueSheet/blue_button10.png"))),
                        },
                        render: ctx => ctx._<Box>(
                            props: new IProp[]
                            {
                                new Prop("position", new StyleEnum<Position>(Position.Absolute)),
                                new Prop("width", new StyleLength(10)),
                                new Prop("height", new StyleLength(10)),
                                new Prop("top", new StyleLength(Length.Percent(50))),
                                new Prop("left", new StyleLength(Length.Percent(50))),
                                new Prop("translate", new StyleTranslate(new Translate(Length.Percent(-50), Length.Percent(-50), 0))),
                                new Prop("backgroundImage", new StyleBackground(resProvider.GetResource<Sprite>("Kenny UI/Spritesheet/greySheet/grey_crossWhite.png"))),
                            }
                        )
                    );
                }
            );
        }
    }
}