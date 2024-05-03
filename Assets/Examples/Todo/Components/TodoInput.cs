using System;
using Examples.Todo.Providers;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
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
            ctx._<StyledInput>(
                props: new IProp[]
                {
                    new EventProp<string>("onChange", HandleChange),
                    new Prop("value", _inputValue),
                    new Prop("height", new StyleLength(50)),
                    new Prop("flexShrink", new StyleFloat(0f)),
                    new Prop("flexGrow", new StyleFloat(1f))
                }
            );
            ctx._<StyledButton>(
                props: new IProp[]
                {
                    new EventProp("onClick", HandleAdd),
                    new Prop("text", "Add"),
                    new Prop("marginLeft", new StyleLength(5)),
                    new Prop("height", new StyleLength(50)),
                    new Prop("width", new StyleLength(100)),
                    new Prop("flexShrink", new StyleFloat(0f)),
                    new Prop("flexGrow", new StyleFloat(0f))
                }
            );
        }
    }
}