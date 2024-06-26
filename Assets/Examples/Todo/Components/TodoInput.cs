﻿using System;
using Examples.Todo.Providers;
using Kekser.ComponentSystem.ComponentBase.StateSystem;
using Kekser.ComponentSystem.ComponentUI;
using UnityEngine.UIElements;

namespace Examples.Todo.Components
{
    public class TodoInput: UIComponent
    {
        private State<string> _inputValue;
        private TodoProvider _todoProvider;
        
        protected override void OnMount()
        {
            _inputValue = UseState("");
            _todoProvider = UseContextProvider<TodoProvider>();
        }

        private void HandleChange(string value)
        {
            _inputValue.Value = value?.Trim();
        }
        
        private void HandleAdd()
        {
            if (string.IsNullOrEmpty(_inputValue.Value))
                return;
            
            _todoProvider.Add(_inputValue.Value);
            _inputValue.Value = "";
        }

        protected override void OnRender()
        {
            _<StyledInput, StyledInputProps>(
                props: new StyledInputProps()
                {
                    onChange = (Action<string>)HandleChange,
                    value = _inputValue.Value,
                    className = "h-50 flex-1 text-nowrap",
                }
            );
            _<StyledButton, StyledButtonProps>(
                props: new StyledButtonProps()
                {
                    onClick = (Action)HandleAdd,
                    text = "Add",
                    className = "h-50 w-100 flex-basis-[100px] flex-0 ml-5",
                }
            );
        }
    }
}