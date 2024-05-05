using System;
using Examples.Todo.Providers;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentUI;
using UnityEngine.UIElements;

namespace Examples.Todo.Components
{
    public class TodoInput: UIComponent
    {
        private string _inputValue = "";
        
        private void HandleChange(string value)
        {
            _inputValue = value?.Trim();
        }
        
        private void HandleAdd()
        {
            if (string.IsNullOrEmpty(_inputValue))
                return;
            
            TodoProvider provider = GetProvider<TodoProvider>();
            provider.Add(_inputValue);
            _inputValue = "";
        }

        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            ctx._<StyledInput, StyledInputProps>(
                props: new StyledInputProps()
                {
                    onChange = (Action<string>)HandleChange,
                    value = _inputValue,
                    style = new Style()
{
                        flexGrow = new StyleFloat(1f),
                        flexShrink = new StyleFloat(1f),
                        height = new StyleLength(50)
                    }
                }
            );
            ctx._<StyledButton, StyledButtonProps>(
                props: new StyledButtonProps()
                {
                    onClick = (Action)HandleAdd,
                    text = "Add",
                    style = new Style()
                    {
                        marginLeft = new StyleLength(5),
                        height = new StyleLength(50),
                        width = new StyleLength(100),
                        flexShrink = new StyleFloat(0f),
                        flexGrow = new StyleFloat(0f)
                    }
                }
            );
        }
    }
}