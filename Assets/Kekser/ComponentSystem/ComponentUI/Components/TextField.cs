using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class TextFieldProps: StyleProps
    {
        public OptionalValue<string> value { get; set; } = new();
        public OptionalValue<Action<string>> onChange { get; set; } = new();
    }
    
    public sealed class TextField: UIComponent<UnityEngine.UIElements.TextField, TextFieldProps>
    {
        private void Change(ChangeEvent<string> eChangeEvent)
        {
            Action<string> e = OwnProps.onChange;
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