using System;
using System.Collections.Generic;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class DropdownFieldProps : StyleProps
    {
        public OptionalValue<string> value { get; set; } = new();
        public OptionalValue<Action<string>> onChange { get; set; } = new();
        public OptionalValue<List<string>> options { get; set; } = new();
    }
    public sealed class DropdownField: UIComponent<UnityEngine.UIElements.DropdownField, DropdownFieldProps>
    {
        public override DropdownFieldProps DefaultProps { get; } = new DropdownFieldProps()
        {
            options = new List<string>(),
        };
        
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
            if (Props.options.IsSet)
                FragmentRoot.choices = Props.options;
            if (Props.value.IsSet)
                FragmentRoot.value = Props.value;
        }
    }
}