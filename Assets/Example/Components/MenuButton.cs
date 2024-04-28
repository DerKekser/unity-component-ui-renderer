using System;
using Example.Providers;
using Kekser.ComponentUI;
using Kekser.ComponentUI.Components;
using Kekser.ComponentUI.PropSystem;
using UnityEngine;

namespace Example.Components
{
    public class MenuButton: UIComponent
    {
        private void HandleClick()
        {
            CountingProvider provider = GetProvider<CountingProvider>();
            provider.Increment();
            
            Props.Get<Action>("onClick")?.Invoke();
        }
        
        public override void OnRender(Context ctx, Action<Context> children)
        {
            ctx._<Button>(
                props: new EventProp("onClick", HandleClick),
                render: ctx => ctx._<Text>(
                    props: new IProp[]
                    {
                        new Prop("text", Props.Get("text")),
                        new Prop("fontSize", 24),
                        new Prop("color", Color.red)
                    }
                )
            );
        }
    }
}