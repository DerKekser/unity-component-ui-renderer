using System;
using Examples.Todo.Components;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.UIElements;

namespace Examples.Todo.Pages
{
    public class TodoProps: StyleProps
    
    {
        public ObligatoryValue<Action> onOptions { get; set; } = new();
    }
    
    public class Todos: UIComponent<TodoProps>
    {
        private void HandleQuit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
            UnityEngine.Application.OpenURL("about:blank");
#else
            UnityEngine.Application.Quit();
#endif
        }
        
        private void HandleOptions()
        {
            Action e = OwnProps.onOptions;
            e?.Invoke();
        }
        
        public override void OnRender()
        {
            _<TodoList, StyleProps>(
                props: new StyleProps(){
                    className = "flex-grow-1"
                }
            );
            _<TodoInput, StyleProps>(
                props: new StyleProps() { 
                    className = "flex-shrink-0 flex-row mt-5",
                }
            );
            _<Group, StyleProps>(
                props: new StyleProps() { 
                    className = "flex-shrink-0 flex-row mt-5",
                },
                render: () =>
                {
                    _<StyledButton, StyledButtonProps>(
                        props: new StyledButtonProps()
                        {
                            onClick = (Action)HandleOptions,
                            text = "Options",
                            className = "h-50 flex-shrink-0 flex-grow-1",
                        }
                    );
                    _<StyledButton, StyledButtonProps>(
                        props: new StyledButtonProps()
                        {
                            onClick = (Action)HandleQuit,
                            text = "Exit",
                            className = "ml-5 h-50 flex-shrink-0 flex-grow-1",
                        }
                    );
                }
            );
            
        }
    }
}