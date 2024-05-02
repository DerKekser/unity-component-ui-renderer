using System;
using Examples.Todo.Components;
using Examples.Todo.Providers;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using UnityEngine.UIElements;
using Box = Kekser.ComponentSystem.ComponentUI.Components.Box;
using Input = Kekser.ComponentSystem.ComponentUI.Components.Input;

namespace Examples.Todo.Pages
{
    public class Todos: UIComponent
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
            Props.Get<Action>("onOptions")?.Invoke();
        }
        
        public override void OnRender(BaseContext<VisualElement> ctx, Action<BaseContext<VisualElement>> children)
        {
            ctx._<TodoList>(
                props: new Prop("flexGrow", new StyleFloat(1f))
            );
            ctx._<TodoInput>(
                props: new IProp[]
                {
                    new Prop("flexShrink", new StyleFloat(0f)),
                    new Prop("flexDirection", new StyleEnum<FlexDirection>(FlexDirection.Row)),
                    new Prop("marginTop", new StyleLength(5)),
                }
            );
            ctx._<Box>(
                props: new IProp[]
                {
                    new Prop("flexShrink", new StyleFloat(0f)),
                    new Prop("flexDirection", new StyleEnum<FlexDirection>(FlexDirection.Row)),
                    new Prop("marginTop", new StyleLength(5)),
                },
                render: ctx =>
                {
                    ctx._<StyledButton>(
                        props: new IProp[]
                        {
                            new EventProp("onClick", HandleOptions),
                            new Prop("text", "Options"),
                            new Prop("height", new StyleLength(50)),
                            new Prop("flexShrink", new StyleFloat(0f)),
                            new Prop("flexGrow", new StyleFloat(1f))
                        }
                    );
                    ctx._<StyledButton>(
                        props: new IProp[]
                        {
                            new EventProp("onClick", HandleQuit),
                            new Prop("text", "Exit"),
                            new Prop("marginLeft", new StyleLength(5)),
                            new Prop("height", new StyleLength(50)),
                            new Prop("flexShrink", new StyleFloat(0f)),
                            new Prop("flexGrow", new StyleFloat(1f))
                        }
                    );
                }
            );
            
        }
    }
}