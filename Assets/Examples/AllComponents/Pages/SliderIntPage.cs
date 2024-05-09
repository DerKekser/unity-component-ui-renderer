using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;

namespace Examples.AllComponents.Pages
{
    public class SliderIntPageProps
    {
        public OptionalValue<int> value { get; set; } = new();
        public OptionalValue<Action<int>> onChange { get; set; } = new();
    }
    
    public class SliderIntPage: UIComponent<SliderIntPageProps>
    {
        public void HandleChange(int selected)
        {
            Props.Set(new SliderIntPageProps() { value = selected });
        }
        
        public override void OnRender()
        {
            _<SliderInt, SliderIntProps>(
                props: new SliderIntProps()
                {
                    onChange = (System.Action<int>)HandleChange,
                    value = OwnProps.value
                }
            );
            _<Label, LabelProps>(props: new LabelProps() { text = $"Selected: {(int)OwnProps.value}" });
        }
    }
}