using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class SliderIntProps: StyleProps
    {
        public OptionalValue<int> value { get; set; } = new();
        public OptionalValue<Action<int>> onChange { get; set; } = new();
    }
    
    public sealed class SliderInt: UIComponent<UnityEngine.UIElements.SliderInt, SliderIntProps>
    {
        private void Change(ChangeEvent<int> eChangeEvent)
        {
            Action<int> e = Props.onChange;
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