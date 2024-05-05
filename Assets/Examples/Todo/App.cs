using System;
using Examples.Todo.Components;
using Examples.Todo.Pages;
using Examples.Todo.Providers;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.UIElements;

namespace Examples.Todo
{
    public class AppProps: StyleProps
    {
        public OptionalValue<App.Pages> page { get; set; } = new();
    }
    
    public class App: UIComponent<AppProps>
    {
        public enum Pages
        {
            Menu,
            Options
        }
        
        private void HandleMenu()
        {
            Props.Set(new AppProps() { page = Pages.Menu });
        }
        
        private void HandleOptions()
        {
            Props.Set(new AppProps() { page = Pages.Options });
        }

        public override AppProps DefaultProps { get; } = new AppProps()
        {
            style = new Style() { height = new StyleLength(Length.Percent(100)) },
            page = Pages.Menu
        };

        public override void OnRender()
        {
            _<Layout, StyleProps>(
                props: new StyleProps() { style = new Style() { height = new StyleLength(Length.Percent(100)) } },
                render: () => 
                {
                    _<TodoProvider>(render: () =>
                    {
                        if (OwnProps.page == Pages.Menu)
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