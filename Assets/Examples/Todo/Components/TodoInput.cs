using System;
using Examples.Todo.Providers;
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

        public override void OnRender()
        {
            _<StyledInput, StyledInputProps>(
                props: new StyledInputProps()
                {
                    onChange = (Action<string>)HandleChange,
                    value = _inputValue,
                    className = "w-50 flex-1",
                }
            );
            _<StyledButton, StyledButtonProps>(
                props: new StyledButtonProps()
                {
                    onClick = (Action)HandleAdd,
                    text = "Add",
                    className = "h-50 w-100 flex-0 ml-5",
                }
            );
        }
    }
}