using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.UIElements;
using Button = Kekser.ComponentSystem.ComponentUI.Components.Button;
using Label = Kekser.ComponentSystem.ComponentUI.Components.Label;

namespace Examples.AllComponents.Pages
{
    public class ButtonPageProps: StyleProps
    {
        public OptionalValue<int> buttonCount { get; set; } = new();
    }
    
    public class ButtonPage: UIComponent<ButtonPageProps>
    {
        public override ButtonPageProps DefaultProps { get; } = new ()
        {
            buttonCount = 0
        };

        public override void OnRender()
        {
            _<Button, ButtonProps>(
                props: new ButtonProps()
                {
                    onClick = new Action(() =>
                    {
                        Props.Set(new ButtonPageProps() { buttonCount = OwnProps.buttonCount + 1 });
                    }),
                    style = new Style()
                    {
                        paddingBottom = new StyleLength(5),
                        paddingLeft = new StyleLength(10),
                        paddingRight = new StyleLength(10),
                        paddingTop = new StyleLength(5),
                        backgroundColor = new StyleColor(new UnityEngine.Color(1f, 1f, 1f, 1)),
                    }
                },
                render: () =>
                {
                    _<Label, LabelProps>(
                        props: new LabelProps()
                        {
                            text = $"Button clicked {(int)OwnProps.buttonCount} times"
                        }
                    );
                }
            );
        }
    }
}