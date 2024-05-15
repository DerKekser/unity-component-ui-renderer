using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.StateSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine;

namespace Examples.AllComponents.Pages
{
    public class MinMaxSliderPage: UIComponent
    {
        private State<float> _min;
        private State<float> _max;
        
        public MinMaxSliderPage()
        {
            _min = CreateState(0f);
            _max = CreateState(100f);
        }
        
        private void HandleChange(float min, float max)
        {
            _min.Value = min;
            _max.Value = max;
        }

        protected override void OnRender()
        {
            _<MinMaxSlider, MinMaxSliderProps>(
                props: new MinMaxSliderProps()
                {
                    onChange = (Action<float, float>)HandleChange,
                    min = _min.Value,
                    max = _max.Value,
                    lowLimit = 25,
                    highLimit = 75,
                }
            );
            _<Label, LabelProps>(props: new LabelProps() { text = $"Min: {_min.Value}, Max: {_max.Value}" });
        }
    }
}