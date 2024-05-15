using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.StateSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;

namespace Examples.AllComponents.Pages
{
    public class TogglePage: UIComponent
    {
        private State<bool> _selected;
        
        public TogglePage()
        {
            _selected = CreateState(false);
        }
        
        public void HandleChange(bool selected)
        {
            _selected.Value = selected;
        }

        protected override void OnRender()
        {
            _<Toggle, ToggleProps>(
                props: new ToggleProps()
                {
                    onChange = (System.Action<bool>)HandleChange,
                    value = _selected.Value
                }
            );
            _<Label, LabelProps>(props: new LabelProps() { text = $"Selected: {_selected.Value}" });
        }
    }
}