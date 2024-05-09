using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class MinMaxSliderProps : StyleProps
    {
        public OptionalValue<float> lowLimit { get; set; } = new();
        public OptionalValue<float> highLimit { get; set; } = new();
        public OptionalValue<float> min { get; set; } = new();
        public OptionalValue<float> max { get; set; } = new();
        public OptionalValue<string> label { get; set; } = new();
        public OptionalValue<Action<float, float>> onChange { get; set; } = new();
    }
    
    public sealed class MinMaxSlider: UIComponent<UnityEngine.UIElements.MinMaxSlider, MinMaxSliderProps>
    {
        private void Change(ChangeEvent<Vector2> e)
        {
            Action<float, float> eAction = OwnProps.onChange;
            eAction?.Invoke(e.newValue.x, e.newValue.y);
        }
        
        public override void OnMount()
        {
            FragmentRoot.RegisterValueChangedCallback(Change);
        }
        
        public override void OnUnmount()
        {
            FragmentRoot.UnregisterValueChangedCallback(Change);
        }

        public override void OnRender()
        {
            if (OwnProps.lowLimit.IsSet)
                FragmentRoot.lowLimit = OwnProps.lowLimit;
            if (OwnProps.highLimit.IsSet)
                FragmentRoot.highLimit = OwnProps.highLimit;
            if (OwnProps.min.IsSet)
                FragmentRoot.minValue = OwnProps.min;
            if (OwnProps.max.IsSet)
                FragmentRoot.maxValue = OwnProps.max;
            if (OwnProps.label.IsSet)
                FragmentRoot.label = OwnProps.label;
            
            Children();
        }
    }
}