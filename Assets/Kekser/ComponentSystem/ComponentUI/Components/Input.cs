using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class InputProps: StyleProps
    {
        public OptionalValue<string> value { get; set; } = new();
        public OptionalValue<Action<string>> onChange { get; set; } = new();
    }
    
    public sealed class Input: UIComponent<TextField, InputProps>
    {
        private void Change(ChangeEvent<string> eChangeEvent)
        {
            Action<string> e = OwnProps.onChange;
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