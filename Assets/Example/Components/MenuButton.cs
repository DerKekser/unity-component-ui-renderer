using System;
using Example.Providers;
using Kekser.ComponentUI;
using Kekser.ComponentUI.Components;
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
                props: props =>
                    props.Set<Action>("onClick", HandleClick),
                render: ctx => ctx._<Text>(props: props =>
                {
                    props.Set("text", Props.Get("text"));
                    props.Set("fontSize", 24);
                    props.Set("color", Color.red);
                })
            );
        }
    }
}