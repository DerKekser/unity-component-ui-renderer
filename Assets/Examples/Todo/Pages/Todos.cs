using System;
using Examples.Todo.Components;
using Examples.Todo.Providers;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.Extension.ResourceManagement;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using UnityEngine;
using UnityEngine.UIElements;
using Box = Kekser.ComponentSystem.ComponentUI.Components.Box;
using Button = Kekser.ComponentSystem.ComponentUI.Components.Button;

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
        
        public override void OnRender(BaseContext<VisualElement> ctx, Action<BaseContext<VisualElement>> children)
        {
            TodoProvider todoProvider = GetProvider<TodoProvider>();
            ResourceProvider<VisualElement> resProvider = GetProvider<ResourceProvider<VisualElement>>();
            
            ctx._<ScrollArea>(
                props: new Prop("flexGrow", new StyleFloat(1f)),
                render: ctx =>
                {
                    ctx.Each(todoProvider.GetTodos(), (todo, i) =>
                    {
                        ctx._<Box>(
                            key: i.ToString(),
                            props: new IProp[]
                            {
                                new Prop("height", new StyleLength(50)),
                                new Prop("flexShrink", new StyleFloat(0f)),
                                new Prop("flexDirection", new StyleEnum<FlexDirection>(FlexDirection.Row)),
                                new Prop("justifyContent", new StyleEnum<Justify>(Justify.SpaceBetween)),
                                new Prop("alignItems", new StyleEnum<Align>(Align.Center)),
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
                                ctx._<Button>(
                                    props: new IProp[]
                                    {
                                        new EventProp("onClick", () => HandleRemove(i)),
                                        new Prop("width", new StyleLength(30)),
                                        new Prop("height", new StyleLength(30)),
                                        new Prop("backgroundImage", new StyleBackground(resProvider.GetResource<Sprite>("Kenny UI/Spritesheet/blueSheet/blue_button10.png"))),
                                    },
                                    render: ctx => ctx._<Box>(
                                        props: new IProp[]
                                        {
                                            new Prop("position", new StyleEnum<Position>(Position.Absolute)),
                                            new Prop("width", new StyleLength(10)),
                                            new Prop("height", new StyleLength(10)),
                                            new Prop("top", new StyleLength(Length.Percent(50))),
                                            new Prop("left", new StyleLength(Length.Percent(50))),
                                            new Prop("translate", new StyleTranslate(new Translate(Length.Percent(-50), Length.Percent(-50), 0))),
                                            new Prop("backgroundImage", new StyleBackground(resProvider.GetResource<Sprite>("Kenny UI/Spritesheet/greySheet/grey_crossWhite.png"))),
                                        }
                                    )
                                );
                            }
                        );
                    });
                }
            );
            /*ctx._<Input>(
                props: new IProp[]
                {
                    new Prop("height", new StyleLength(50)),
                    new Prop("flexShrink", new StyleFloat(0f)),
                }
            );*/
            ctx._<StyledButton>(
                props: new IProp[]
                {
                    new EventProp("onClick", HandleAdd),
                    new Prop("text", "Add todo"),
                    new Prop("height", new StyleLength(50)),
                    new Prop("marginTop", new StyleLength(5)),
                    new Prop("flexShrink", new StyleFloat(0f)),
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