using System;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.Extension.ResourceManagement;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.PropSystem.Rework;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using UnityEngine;
using UnityEngine.UIElements;
using Button = Kekser.ComponentSystem.ComponentUI.Components.Button;

namespace Examples.Todo.Components
{
    public struct StyledButtonProps
    {
        public OptionalValue<Action> onClick { get; set; }
        public OptionalValue<string> text { get; set; }
        public OptionalValue<Style> style { get; set; }
    }
    
    public class StyledButton: UIComponent<StyledButtonProps>
    {
        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            ResourceProvider<VisualElement> provider = GetProvider<ResourceProvider<VisualElement>>();
            
            ctx._<Button, ButtonProps>(
                props: new ButtonProps()
                {
                    onClick = Props.Get<StyledButtonProps>().onClick,
                    style = new Style()
                    {
                        width = new StyleLength(Length.Percent(100)),
                        height = new StyleLength(Length.Percent(100)),
                        backgroundImage = new StyleBackground(provider.GetResource<Sprite>("Kenny UI/Spritesheet/blueSheet/blue_button11.png")),
                    }
                },
                render: ctx => ctx._<Text, TextProps>(
                    props: new TextProps()
                    {
                        text = Props.Get<StyledButtonProps>().text,
                        style = new Style()
                        {
                            width = new StyleLength(Length.Percent(100)),
                            height = new StyleLength(Length.Percent(100)),
                            fontSize = new StyleLength(24),
                            color = new StyleColor(Color.white),
                        }
                    }
                )
            );
        }
    }
}