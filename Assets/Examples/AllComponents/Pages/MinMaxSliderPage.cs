using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine;

namespace Examples.AllComponents.Pages
{
    public class MinMaxSliderPageProps : StyleProps
    {
        public OptionalValue<float> min { get; set; } = new();
        public OptionalValue<float> max { get; set; } = new();
    }
    
    public class MinMaxSliderPage: UIComponent<MinMaxSliderPageProps>
    {
        public void HandleChange(float min, float max)
        {
            Props.Set(new MinMaxSliderPageProps() { min = min, max = max });
        }
        
        public override void OnRender()
        {
            _<MinMaxSlider, MinMaxSliderProps>(
                props: new MinMaxSliderProps()
                {
                    onChange = (Action<float, float>)HandleChange,
                    min = OwnProps.min,
                    max = OwnProps.max,
                    lowLimit = 25,
                    highLimit = 75,
                }
            );
            _<Label, LabelProps>(props: new LabelProps() { text = $"Min: {(float)OwnProps.min}, Max: {(float)OwnProps.max}" });
        }
    }
}