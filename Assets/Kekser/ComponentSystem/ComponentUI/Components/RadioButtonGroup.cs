using System;
using System.Collections.Generic;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class RadioButtonGroupProps: StyleProps
    {
        public OptionalValue<int> value { get; set; } = new();
        public OptionalValue<Action<int>> onChange { get; set; } = new();
        public OptionalValue<List<string>> options { get; set; } = new();
    }
    
    public sealed class RadioButtonGroup: UIComponent<UnityEngine.UIElements.RadioButtonGroup, RadioButtonGroupProps>
    {
        public override RadioButtonGroupProps DefaultProps { get; } = new RadioButtonGroupProps()
        {
            options = new List<string>(),
        };
        
        private void Change(ChangeEvent<int> e)
        {
            Action<int> eAction = OwnProps.onChange;
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
            if (OwnProps.options.IsSet)
                FragmentRoot.choices = (List<string>)OwnProps.options;
            if (OwnProps.value.IsSet)
                FragmentRoot.value = OwnProps.value;
        }
    }
}