using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine;
using UnityEngine.UIElements;
using Button = Kekser.ComponentSystem.ComponentUI.Components.Button;
using Label = Kekser.ComponentSystem.ComponentUI.Components.Label;

namespace Examples.Todo.Components
{
    public class StyledButtonProps: StyleProps
    {
        public OptionalValue<Action> onClick { get; set; } = new();
        public OptionalValue<string> text { get; set; } = new();
    }
    
    public class StyledButton: UIComponent<StyledButtonProps>
    {
        protected override void OnRender()
        {
            _<Button, ButtonProps>(
                props: new ButtonProps()
                {
                    onClick = Props.onClick,
                    className = "bg-[/Assets/Kenny%20UI/Spritesheet/blueSheet.png#blue_button11.png] hover:bg-[/Assets/Kenny%20UI/Spritesheet/blueSheet.png#blue_button12.png] w-[100%] h-[100%]",
                },
                render: () => _<Label, LabelProps>(
                    props: new LabelProps()
                    {
                        text = Props.text,
                        className = "w-[100%] h-[100%] font-24 color-white text-align-middle-center",
                    }
                )
            );
        }
    }
}