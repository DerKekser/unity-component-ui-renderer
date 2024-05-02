using System;
using Examples.Todo.Components;
using Examples.Todo.Pages;
using Examples.Todo.Providers;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
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

        public override void OnRender(BaseContext<VisualElement> ctx, Action<BaseContext<VisualElement>> children)
        {
            ctx._<Layout>(
                props: new Prop("height", new StyleLength(Length.Percent(100))),
                render: ctx => 
                {
                    ctx._<TodoProvider>(render: ctx =>
                    {
                        if (Props.Get<Pages>("page") == Pages.Menu)
                        {
                            ctx._<Todos>(
                                props: new EventProp("onOptions", HandleOptions)
                            );
                        }
                        else
                        {
                            ctx._<Options>(
                                props: new EventProp("onBack", HandleMenu)
                            );
                        }
                    });
                }
            );
        }
    }
}