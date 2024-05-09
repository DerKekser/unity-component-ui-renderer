using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class SliderProps: StyleProps
    {
        public OptionalValue<float> value { get; set; } = new();
        public OptionalValue<Action<float>> onChange { get; set; } = new();
    }
    
    public sealed class Slider: UIComponent<UnityEngine.UIElements.Slider, SliderProps>
    {
        private void Change(ChangeEvent<float> eChangeEvent)
        {
            Action<float> e = OwnProps.onChange;
            e?.Invoke(eChangeEvent.newValue);
        }
        
        public override void OnMount()
        {
            FragmentNode.RegisterValueChangedCallback(Change);
        }
        
        public override void OnUnmount()
        {
            FragmentNode.UnregisterValueChangedCallback(Change);
        }
        
        public override void OnRender()
        {
            if (OwnProps.value.IsSet)
                FragmentNode.value = OwnProps.value;
        }
    }
}