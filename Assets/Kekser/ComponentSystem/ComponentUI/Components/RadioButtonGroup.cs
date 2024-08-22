using System;
using System.Collections.Generic;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class RadioButtonGroupProps: StyleProps
    {
        public OptionalValue<int> value { get; set; } = new();
        public OptionalValue<Action<int>> onChange { get; set; } = new();
        public OptionalValue<List<string>> options { get; set; } = new();
    }
    
    [Preserve]
    public sealed class RadioButtonGroup: UIComponent<UnityEngine.UIElements.RadioButtonGroup, RadioButtonGroupProps>
    {
        public override RadioButtonGroupProps DefaultProps { get; } = new RadioButtonGroupProps()
        {
            options = new List<string>(),
        };
        
        private void Change(ChangeEvent<int> e)
        {
            Action<int> eAction = Props.onChange;
            eAction?.Invoke(e.newValue);
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
                FragmentRoot.choices = (List<string>)Props.options;
            if (Props.value.IsSet)
                FragmentRoot.value = Props.value;
        }
    }
}