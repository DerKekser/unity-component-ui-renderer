using System;
using Scenes.Kekser.ComponentUI;
using Scenes.Kekser.ComponentUI.Components;
using Scenes.Test.Components;

namespace Scenes.Test.Pages
{
    public class Options: UIComponent
    {
        private void HandleBack()
        {
            Props.Get<Action>("onBack")?.Invoke();
        }
        
        public override void OnRender(Context ctx, Action<Context> children)
        {
            ctx._<VerticalLayout>(
                props: props => props.Set("spacing", 10),
                render: ctx =>
                {
                    ctx._<MenuButton>(
                        props: props =>
                        {
                            props.Set("text", "Option 1");
                        }
                    );
                    ctx._<MenuButton>(
                        props: props =>
                        {
                            props.Set("text", "Option 2");
                        }
                    );
                    ctx._<MenuButton>(
                        props: props =>
                        {
                            props.Set("text", "Option 3");
                        }
                    );
                    ctx._<MenuButton>(
                        props: props =>
                        {
                            props.Set("text", "Option 4");
                        }
                    );
                    ctx._<MenuButton>(
                        props: props =>
                        {
                            props.Set<Action>("onClick", HandleBack);
                            props.Set("text", "Back");
                        }
                    );
                });
        }
    }
}