using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;

namespace Examples.AllComponents.Pages
{
    public class TextFieldPageProps
    {
        public OptionalValue<string> value { get; set; } = new();
    }
    
    public class TextFieldPage: UIComponent<TextFieldPageProps>
    {
        public void HandleChange(string selected)
        {
            Props.Set(new TextFieldPageProps() { value = selected });
        }
        
        public override void OnRender()
        {
            _<TextField, TextFieldProps>(
                props: new TextFieldProps()
                {
                    onChange = (System.Action<string>)HandleChange,
                    value = OwnProps.value
                }
            );
            _<Label, LabelProps>(props: new LabelProps() { text = $"Selected: {(string)OwnProps.value}" });
        }
    }
}