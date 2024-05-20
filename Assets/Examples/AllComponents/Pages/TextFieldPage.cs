using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.StateSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;

namespace Examples.AllComponents.Pages
{
    public class TextFieldPage: UIComponent
    {
        private State<string> _text;

        protected override void OnMount()
        {
            _text = UseState("");
        }

        private void HandleChange(string selected)
        {
            _text.Value = selected;
        }

        protected override void OnRender()
        {
            _<TextField, TextFieldProps>(
                props: new TextFieldProps()
                {
                    onChange = (System.Action<string>)HandleChange,
                    value = _text.Value,
                }
            );
            _<Label, LabelProps>(props: new LabelProps() { text = $"Selected: {_text.Value}" });
        }
    }
}