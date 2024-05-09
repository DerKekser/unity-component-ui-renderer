using System.Collections.Generic;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;

namespace Examples.AllComponents.Pages
{
    public class RadioButtonGroupPageProps
    {
        public OptionalValue<int> value { get; set; } = new();
    }
    
    public class RadioButtonGroupPage: UIComponent<RadioButtonGroupPageProps>
    {
        public void HandleChange(int selected)
        {
            Props.Set(new RadioButtonGroupPageProps() { value = selected });
        }
        
        public override void OnRender()
        {
            _<RadioButtonGroup, RadioButtonGroupProps>(
                props: new RadioButtonGroupProps()
                {
                    onChange = (System.Action<int>)HandleChange,
                    value = OwnProps.value,
                    options = new List<string>() { "Option 1", "Option 2", "Option 3" }
                }
            );
            _<Label, LabelProps>(props: new LabelProps() { text = $"Selected: {(int)OwnProps.value}" });
        }
    }
}