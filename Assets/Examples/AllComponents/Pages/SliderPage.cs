using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.StateSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;

namespace Examples.AllComponents.Pages
{
    public class SliderPage: UIComponent
    {
        private State<float> _selected;
        
        public SliderPage()
        {
            _selected = CreateState(0f);
        }
        
        private void HandleChange(float selected)
        {
            _selected.Value = selected;
        }

        protected override void OnRender()
        {
            _<Slider, SliderProps>(
                props: new SliderProps()
                {
                    onChange = (System.Action<float>)HandleChange,
                    value = _selected.Value,
                }
            );
            _<Label, LabelProps>(props: new LabelProps() { text = $"Selected: {_selected.Value}" });
        }
    }
}