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
                props: new StyleProps() { style = new Style() { flexGrow = new StyleFloat(1f) } }
            );
            _<TodoInput, StyleProps>(
                props: new StyleProps() { style = new Style()
                {
                    flexShrink = new StyleFloat(0f),
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row),
                    marginTop = new StyleLength(5)
                }}
            );
            _<Group, StyleProps>(
                props: new StyleProps() { style = new Style()
                {
                    flexShrink = new StyleFloat(0f),
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row),
                    marginTop = new StyleLength(5)
                }},
                render: () =>
                {
                    _<StyledButton, StyledButtonProps>(
                        props: new StyledButtonProps()
                        {
                            onClick = (Action)HandleOptions,
                            text = "Options",
                            style = new Style()
                            {
                                height = new StyleLength(50),
                                flexShrink = new StyleFloat(0f),
                                flexGrow = new StyleFloat(1f)
                            }
                        }
                    );
                    _<StyledButton, StyledButtonProps>(
                        props: new StyledButtonProps()
                        {
                            onClick = (Action)HandleQuit,
                            text = "Exit",
                            style = new Style()
                            {
                                marginLeft = new StyleLength(5),
                                height = new StyleLength(50),
                                flexShrink = new StyleFloat(0f),
                                flexGrow = new StyleFloat(1f)
                            }
                        }
                    );
                }
            );
            
        }
    }
}