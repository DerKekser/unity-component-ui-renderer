using System;
using Example.Pages;
using Kekser.ComponentUI;

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
            if (Props.Get<Pages>("page") == Pages.Menu)
            {
                ctx._<Menu>(
                    props: props => props.Set<Action>("onOptions", HandleOptions)
                );
            }
            else
            {
                ctx._<Options>(
                    props: props => props.Set<Action>("onBack", HandleMenu)
                );
            }
        }
    }
}