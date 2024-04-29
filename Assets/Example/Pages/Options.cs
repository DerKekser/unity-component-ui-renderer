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
                    ctx.Each(new int[5], (x, i) => 
                    {
                        ctx._<StyledButton>(
                            key: i.ToString(),
                            props: new Prop("text", $"Option {i}")
                        );
                    });
                    ctx._<StyledButton>(
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