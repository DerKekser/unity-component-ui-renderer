using System;
using Example.Components;
using Example.Providers;
using Kekser.ComponentUI;
using Kekser.ComponentUI.Components;
using Kekser.ComponentUI.PropSystem;

namespace Example.Pages
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
            
            ctx._<Box>(
                props: new IProp[]
                {
                    new Prop("childControlHeight", false),
                    new Prop("childForceExpandHeight", false),
                    new Prop("spacing", 10),
                },
                render: ctx =>
                {
                    ctx.Each(provider.GetTodos(), (todo, i) =>
                    {
                        ctx._<Box>(
                            key: i.ToString(),
                            props: new Prop("height", "50px"),
                            render: ctx =>
                            {
                                ctx._<Text>(
                                    props: new IProp[]
                                    {
                                        new Prop("text", todo),
                                        new Prop("left", "0px"),
                                        new Prop("right", "10%"),
                                    }
                                );
                                ctx._<StyledButton>(
                                    props: new IProp[]
                                    {
                                        new EventProp("onClick", () => HandleRemove(i)),
                                        new Prop("text", "X"),
                                        new Prop("right", "0px"),
                                        new Prop("width", "10%"),
                                    }
                                );
                            }
                        );
                    });
                    ctx._<StyledButton>(
                        props: new IProp[]
                        {
                            new EventProp("onClick", HandleAdd),
                            new Prop("text", "Add todo"),
                            new Prop("height", "50px"),
                        }
                    );
                    ctx._<StyledButton>(
                        props: new IProp[]
                        {
                            new EventProp("onClick", HandleOptions),
                            new Prop("text", "Options"),
                            new Prop("height", "50px"),
                        }
                    );
                    ctx._<StyledButton>(
                        props: new IProp[]
                        {
                            new EventProp("onClick", HandleQuit),
                            new Prop("text", "Exit"),
                            new Prop("height", "50px"),
                        }
                    );
                }
            );
        }
    }
}