using System;
using Examples.Todo.Components;
using Examples.Todo.Pages;
using Examples.Todo.Providers;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.StateSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.UIProps;
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

        private State<Pages> _page;

        public App()
        {
            _page = CreateState(Pages.Menu);
        }
        
        private void HandleMenu()
        {
            _page.Value = Pages.Menu;
        }
        
        private void HandleOptions()
        {
            _page.Value = Pages.Options;
        }

        public override StyleProps DefaultProps { get; } = new StyleProps()
        {
            style = new Style() { height = new StyleLength(Length.Percent(100)) },
        };

        protected override void OnRender()
        {
            _<Layout, StyleProps>(
                props: new StyleProps() { style = new Style() { height = new StyleLength(Length.Percent(100)) } },
                render: () => 
                {
                    _<TodoProvider>(render: () =>
                    {
                        if (_page.Value == Pages.Menu)
                        {
                            _<Todos, TodoProps>(
                                props: new TodoProps()
                                {
                                    onOptions = (Action)HandleOptions,
                                    style = new Style() { flexGrow = new StyleFloat(1f) }
                                }
                            );
                        }
                        else
                        {
                            _<Options, OptionsProps>(
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