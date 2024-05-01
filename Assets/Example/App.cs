using System;
using Example.Components;
using Example.Pages;
using Example.Providers;
using Kekser.ComponentUI;
using Kekser.ComponentUI.PropSystem;
using UnityEngine.UIElements;

namespace Example
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

        public override void OnRender(Context ctx, Action<Context> children)
        {
            ctx._<Layout>(render: ctx => 
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
            });
        }
    }
}