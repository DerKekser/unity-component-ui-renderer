using System;
using System.Collections.Generic;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.UIProps;

namespace Examples.AllComponents.Pages
{
    public class DropdownFieldPageProps : StyleProps
    {
        public OptionalValue<string> value { get; set; } = new();
    }
    
    public class DropdownFieldPage: UIComponent<DropdownFieldPageProps>
    {
        public void HandleChange(string value)
        {
            Props.Set(new DropdownFieldPageProps() { value = value });
        }
        
        public override void OnRender()
        {
            _<DropdownField, DropdownFieldProps>(
                props: new DropdownFieldProps()
                {
                    value = OwnProps.value,
                    onChange = new Action<string>(HandleChange),
                    options = new List<string>()
                    {
                        "Option 1",
                        "Option 2",
                        "Option 3",
                        "Option 4",
                        "Option 5",
                    }
                }
            );
            _<Label, LabelProps>(
                props: new LabelProps() { text = $"Selected value: {(string)OwnProps.value}" }
            );
        }
    }
}