using System.Collections.Generic;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.StateSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;

namespace Examples.AllComponents.Pages
{
    public class RadioButtonGroupPage: UIComponent
    {
        private State<int> _selected;
        
        protected override void OnMount()
        {
            _selected = UseState(0);
        }

        private void HandleChange(int selected)
        {
            _selected.Value = selected;
        }

        protected override void OnRender()
        {
            _<RadioButtonGroup, RadioButtonGroupProps>(
                props: new RadioButtonGroupProps()
                {
                    onChange = (System.Action<int>)HandleChange,
                    value = _selected.Value,
                    options = new List<string>() { "Option 1", "Option 2", "Option 3" }
                }
            );
            _<Label, LabelProps>(props: new LabelProps() { text = $"Selected: {_selected.Value}" });
        }
    }
}