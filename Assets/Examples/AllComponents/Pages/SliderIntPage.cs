using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.StateSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;

namespace Examples.AllComponents.Pages
{
    public class SliderIntPage: UIComponent
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
            _<SliderInt, SliderIntProps>(
                props: new SliderIntProps()
                {
                    onChange = (System.Action<int>)HandleChange,
                    value = _selected.Value,
                }
            );
            _<Label, LabelProps>(props: new LabelProps() { text = $"Selected: {_selected.Value}" });
        }
    }
}