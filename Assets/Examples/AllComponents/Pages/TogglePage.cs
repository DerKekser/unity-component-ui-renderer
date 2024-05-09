using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;

namespace Examples.AllComponents.Pages
{
    public class TogglePageProps
    {
        public OptionalValue<bool> value { get; set; } = new();
    }
    
    public class TogglePage: UIComponent<TogglePageProps>
    {
        public void HandleChange(bool selected)
        {
            Props.Set(new TogglePageProps() { value = selected });
        }
        
        public override void OnRender()
        {
            _<Toggle, ToggleProps>(
                props: new ToggleProps()
                {
                    onChange = (System.Action<bool>)HandleChange,
                    value = OwnProps.value
                }
            );
            _<Label, LabelProps>(props: new LabelProps() { text = $"Selected: {(bool)OwnProps.value}" });
        }
    }
}