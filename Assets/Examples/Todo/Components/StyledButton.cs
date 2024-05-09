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
        public override void OnRender()
        {
            _<Button, ButtonProps>(
                props: new ButtonProps()
                {
                    onClick = OwnProps.onClick,
                    className = "unity-button todo-button",
                    style = new Style()
                    {
                        width = new StyleLength(Length.Percent(100)),
                        height = new StyleLength(Length.Percent(100)),
                    }
                },
                render: () => _<Label, LabelProps>(
                    props: new LabelProps()
                    {
                        text = OwnProps.text,
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