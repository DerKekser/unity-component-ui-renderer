using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;

namespace Examples.AllComponents.Pages
{
    public class RadioButtonPageProps
    {
        public OptionalValue<bool> value { get; set; } = new();
    }
    
    public class RadioButtonPage: UIComponent<RadioButtonPageProps>
    {
        public void HandleChange(bool selected)
        {
            Props.Set(new RadioButtonPageProps() { value = selected });
        }
        
        public override void OnRender()
        {
            _<RadioButton, RadioButtonProps>(
                props: new RadioButtonProps()
                {
                    onChange = (System.Action<bool>)HandleChange,
                    value = OwnProps.value
                }
            );
            _<Label, LabelProps>(props: new LabelProps() { text = $"Selected: {(bool)OwnProps.value}" });
        }
    }
}