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
            Action<int> e = OwnProps.onChange;
            e?.Invoke(eChangeEvent.newValue);
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
            if (OwnProps.value.IsSet)
                FragmentRoot.value = OwnProps.value;
        }
    }
}