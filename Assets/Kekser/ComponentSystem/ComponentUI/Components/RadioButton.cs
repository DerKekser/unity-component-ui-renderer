using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class RadioButtonProps: StyleProps
    {
        public OptionalValue<string> text { get; set; } = new();
        public OptionalValue<bool> value { get; set; } = new();
        public OptionalValue<Action<bool>> onChange { get; set; } = new();
    }
    
    public sealed class RadioButton: UIComponent<UnityEngine.UIElements.RadioButton, RadioButtonProps>
    {
        private void Change(ChangeEvent<bool> e)
        {
            Action<bool> eAction = OwnProps.onChange;
            eAction?.Invoke(e.newValue);
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
            if (OwnProps.text.IsSet)
                FragmentRoot.text = OwnProps.text;
            if (OwnProps.value.IsSet)
                FragmentRoot.value = OwnProps.value;
            
            Children();
        }
    }
}