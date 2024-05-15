using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.StateSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.UIElements;
using Button = Kekser.ComponentSystem.ComponentUI.Components.Button;
using Label = Kekser.ComponentSystem.ComponentUI.Components.Label;

namespace Examples.AllComponents.Pages
{
    public class ButtonPage: UIComponent
    {
        private State<int> _buttonCount;
        
        public ButtonPage()
        {
            _buttonCount = CreateState(0);
        }

        protected override void OnRender()
        {
            _<Button, ButtonProps>(
                props: new ButtonProps()
                {
                    onClick = new Action(() =>
                    {
                        _buttonCount.Value++;
                    }),
                    className = "bg-white w-[100%] p-10 text-center hover:bg-[#f0f0f0]"
                },
                render: () =>
                {
                    _<Label, LabelProps>(
                        props: new LabelProps()
                        {
                            text = $"Button clicked {_buttonCount.Value} times"
                        }
                    );
                }
            );
        }
    }
}