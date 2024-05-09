using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;

namespace Examples.AllComponents.Pages
{
    public class SliderPageProps
    {
        public OptionalValue<float> value { get; set; } = new();
    }
    
    public class SliderPage: UIComponent<SliderPageProps>
    {
        public void HandleChange(float selected)
        {
            Props.Set(new SliderPageProps() { value = selected });
        }
        
        public override void OnRender()
        {
            _<Slider, SliderProps>(
                props: new SliderProps()
                {
                    onChange = (System.Action<float>)HandleChange,
                    value = OwnProps.value
                }
            );
            _<Label, LabelProps>(props: new LabelProps() { text = $"Selected: {(float)OwnProps.value}" });
        }
    }
}