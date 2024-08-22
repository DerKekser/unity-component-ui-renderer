using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class TextFieldProps: StyleProps
    {
        public OptionalValue<string> value { get; set; } = new();
        public OptionalValue<Action<string>> onChange { get; set; } = new();
    }
    
    [Preserve]
    public sealed class TextField: UIComponent<UnityEngine.UIElements.TextField, TextFieldProps>
    {
        private void Change(ChangeEvent<string> eChangeEvent)
        {
            Action<string> e = Props.onChange;
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