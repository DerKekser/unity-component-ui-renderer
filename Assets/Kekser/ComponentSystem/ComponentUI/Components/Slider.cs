using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class SliderProps: StyleProps
    {
        public OptionalValue<float> value { get; set; } = new();
        public OptionalValue<Action<float>> onChange { get; set; } = new();
    }
    
    [Preserve]
    public sealed class Slider: UIComponent<UnityEngine.UIElements.Slider, SliderProps>
    {
        private void Change(ChangeEvent<float> eChangeEvent)
        {
            Action<float> e = Props.onChange;
            e?.Invoke(eChangeEvent.newValue);
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
            if (Props.value.IsSet)
                FragmentRoot.value = Props.value;
        }
    }
}