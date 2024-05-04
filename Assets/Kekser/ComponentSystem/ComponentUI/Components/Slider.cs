using System;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.PropSystem.Rework;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public struct SliderProps
    {
        public OptionalValue<float> value { get; set; }
        public OptionalValue<Action<float>> onChange { get; set; }
        public OptionalValue<Style> style { get; set; }
    }
    
    public class Slider: UIComponent<UnityEngine.UIElements.Slider>
    {
        private void Change(ChangeEvent<float> eChangeEvent)
        {
            Action<float> e = Props.Get<SliderProps>().onChange;
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
        
        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            SliderProps props = Props.Get<SliderProps>();
            if (props.value.IsSet)
                FragmentNode.value = props.value;
        }
    }
}