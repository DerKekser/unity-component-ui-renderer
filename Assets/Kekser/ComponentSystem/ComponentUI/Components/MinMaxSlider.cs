using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine;
using UnityEngine.Scripting;
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
    
    [Preserve]
    public sealed class MinMaxSlider: UIComponent<UnityEngine.UIElements.MinMaxSlider, MinMaxSliderProps>
    {
        private void Change(ChangeEvent<Vector2> e)
        {
            Action<float, float> eAction = Props.onChange;
            eAction?.Invoke(e.newValue.x, e.newValue.y);
        }

        protected override void OnMount()
        {
            FragmentRoot.RegisterValueChangedCallback(Change);
        }

        protected override void OnUnmount()
        {
            FragmentRoot.UnregisterValueChangedCallback(Change);
        }

        protected override void OnRender()
        {
            if (Props.lowLimit.IsSet)
                FragmentRoot.lowLimit = Props.lowLimit;
            if (Props.highLimit.IsSet)
                FragmentRoot.highLimit = Props.highLimit;
            if (Props.min.IsSet)
                FragmentRoot.minValue = Props.min;
            if (Props.max.IsSet)
                FragmentRoot.maxValue = Props.max;
            if (Props.label.IsSet)
                FragmentRoot.label = Props.label;
            
            Children();
        }
    }
}