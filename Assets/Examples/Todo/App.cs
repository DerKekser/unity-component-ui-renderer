using System;
using Examples.Todo.Components;
using Examples.Todo.Pages;
using Examples.Todo.Providers;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.PropSystem.Rework;
using Kekser.ComponentSystem.ComponentUI;
using UnityEngine.UIElements;

namespace Examples.Todo
{
    public class App: UIComponent
    {
        private enum Pages
        {
            Menu,
            Options
        }
        
        private void HandleMenu()
        {
            Props.Set("page", Pages.Menu);
        }
        
        private void HandleOptions()
        {
            Props.Set("page", Pages.Options);
        }

        public override void OnMount()
        {
            Props.Set("page", Pages.Menu);
        }

        /*public override IProp[] DefaultProps => new IProp[]
        {
            new Prop("height", new StyleLength(Length.Percent(100))),
            new Prop("page", Pages.Menu),
        };*/

        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            ctx._<Layout, StyleProps>(
                props: new StyleProps() { style = new Style() { height = new StyleLength(Length.Percent(100)) } },
                render: ctx => 
                {
                    ctx._<TodoProvider>(render: ctx =>
                    {
                        if (Props.Get<Pages>("page") == Pages.Menu)
                        {
                            ctx._<Todos, TodoProps>(
                                props: new TodoProps()
                                {
                                    onOptions = (Action)HandleOptions,
                                    style = new Style() { flexGrow = new StyleFloat(1f) }
                                }
                            );
                        }
                        else
                        {
                            ctx._<Options, OptionsProps>(
                                props: new OptionsProps()
                                {
                                    onBack = (Action)HandleMenu,
                                    style = new Style() { flexGrow = new StyleFloat(1f) }
                                }
                            );
                        }
                    });
                }
            );
        }
    }
}