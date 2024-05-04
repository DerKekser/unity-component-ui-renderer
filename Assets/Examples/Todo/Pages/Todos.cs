using System;
using Examples.Todo.Components;
using Examples.Todo.Providers;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.PropSystem.Rework;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using UnityEngine.UIElements;
using Box = Kekser.ComponentSystem.ComponentUI.Components.Box;
using Input = Kekser.ComponentSystem.ComponentUI.Components.Input;

namespace Examples.Todo.Pages
{
    public struct TodoProps
    {
        public ObligatoryValue<Action> onOptions { get; set; }
        public OptionalValue<Style> style { get; set; }
    }
    
    public class Todos: UIComponent<TodoProps>
    {
        private void HandleQuit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
            Application.OpenURL("about:blank");
#else
            Application.Quit();
#endif
        }
        
        private void HandleOptions()
        {
            Action e = OwnProps.onOptions;
            e?.Invoke();
        }
        
        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            ctx._<TodoList, StyleProps>(
                props: new StyleProps() { style = new Style() { flexGrow = new StyleFloat(1f) } }
            );
            ctx._<TodoInput, StyleProps>(
                props: new StyleProps() { style = new Style()
                {
                    flexShrink = new StyleFloat(0f),
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row),
                    marginTop = new StyleLength(5)
                }}
            );
            ctx._<Box, StyleProps>(
                props: new StyleProps() { style = new Style()
                {
                    flexShrink = new StyleFloat(0f),
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row),
                    marginTop = new StyleLength(5)
                }},
                render: ctx =>
                {
                    ctx._<StyledButton, StyledButtonProps>(
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
                    ctx._<StyledButton, StyledButtonProps>(
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