using System;
using Example.Components;
using Kekser.ComponentUI;
using Kekser.ComponentUI.Components;
using Kekser.ComponentUI.PropSystem;

namespace Example.Pages
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
                props: new Prop("spacing", 10),
                render: ctx =>
                {
                    ctx._<MenuButton>(
                        props: new Prop("text", "Option 1")
                    );
                    ctx._<MenuButton>(
                        props: new Prop("text", "Option 2")
                    );
                    ctx._<MenuButton>(
                        props: new Prop("text", "Option 3")
                    );
                    ctx._<MenuButton>(
                        props: new Prop("text", "Option 4")
                    );
                    ctx._<MenuButton>(
                        props: new IProp[]
                        {
                            new EventProp("onClick", HandleBack),
                            new Prop("text", "Back")
                        }
                    );
                }
            );
        }
    }
}