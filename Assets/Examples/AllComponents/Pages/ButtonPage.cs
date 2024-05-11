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
                    className = "bg-white w-[100%] p-10 text-center hover:bg-[#f0f0f0]"
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