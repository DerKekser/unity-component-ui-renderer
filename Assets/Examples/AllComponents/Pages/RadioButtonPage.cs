using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.StateSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;

namespace Examples.AllComponents.Pages
{
    public class RadioButtonPage: UIComponent
    {
        private State<bool> _selected;
        
        public RadioButtonPage()
        {
            _selected = CreateState(false);
        }
        
        private void HandleChange(bool selected)
        {
            _selected.Value = selected;
        }

        protected override void OnRender()
        {
            _<RadioButton, RadioButtonProps>(
                props: new RadioButtonProps()
                {
                    onChange = (System.Action<bool>)HandleChange,
                    value = _selected.Value
                }
            );
            _<Label, LabelProps>(props: new LabelProps() { text = $"Selected: {_selected.Value}" });
        }
    }
}