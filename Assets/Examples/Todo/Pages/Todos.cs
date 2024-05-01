using System;
using Examples.Todo.Components;
using Examples.Todo.Providers;
using Kekser.ComponentUI;
using Kekser.ComponentUI.Components;
using Kekser.ComponentUI.PropSystem;
using UnityEngine.UIElements;
using Box = Kekser.ComponentUI.Components.Box;

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
        
        private void HandleRemove(int index)
        {
            TodoProvider provider = GetProvider<TodoProvider>();
            provider.Remove(index);
        }
        
        private void HandleAdd()
        {
            TodoProvider provider = GetProvider<TodoProvider>();
            provider.Add($"New todo {provider.GetCount() + 1}");
        }
        
        public override void OnRender(Context ctx, Action<Context> children)
        {
            TodoProvider provider = GetProvider<TodoProvider>();
            
            ctx._<ScrollArea>(
                render: ctx =>
                {
                    ctx.Each(provider.GetTodos(), (todo, i) =>
                    {
                        ctx._<Box>(
                            key: i.ToString(),
                            props: new IProp[]
                            {
                                new Prop("height", new StyleLength(50)),
                                new Prop("flexDirection", new StyleEnum<FlexDirection>(FlexDirection.Row)),
                                new Prop("justifyContent", new StyleEnum<Justify>(Justify.SpaceBetween)),
                            },
                            render: ctx =>
                            {
                                ctx._<Text>(
                                    props: new IProp[]
                                    {
                                        new Prop("text", todo),
                                        new Prop("flexGrow", new StyleFloat(1)),
                                    }
                                );
                                ctx._<StyledButton>(
                                    props: new IProp[]
                                    {
                                        new EventProp("onClick", () => HandleRemove(i)),
                                        new Prop("text", "X"),
                                        new Prop("width", new StyleLength(50)),
                                    }
                                );
                            }
                        );
                    });
                }
            );
            ctx._<Input>();
            ctx._<StyledButton>(
                props: new IProp[]
                {
                    new EventProp("onClick", HandleAdd),
                    new Prop("text", "Add todo"),
                    new Prop("height", new StyleLength(50))
                }
            );
            ctx._<StyledButton>(
                props: new IProp[]
                {
                    new EventProp("onClick", HandleOptions),
                    new Prop("text", "Options"),
                    new Prop("height", new StyleLength(50))
                }
            );
            ctx._<StyledButton>(
                props: new IProp[]
                {
                    new EventProp("onClick", HandleQuit),
                    new Prop("text", "Exit"),
                    new Prop("height", new StyleLength(50))
                }
            );
        }
    }
}