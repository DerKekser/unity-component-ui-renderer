using System;
using Example.Components;
using Example.Providers;
using Kekser.ComponentUI;
using Kekser.ComponentUI.Components;
using Kekser.ComponentUI.PropSystem;

namespace Example.Pages
{
    public class Menu: UIComponent
    {
        private int _clicks = 0;

        private void HandleQuit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
            Application.OpenURL("about:blank");
#else
            Application.Quit();
#endif
        }
        
        private void HandleOptions()
        {
            Props.Get<Action>("onOptions")?.Invoke();
        }
        
        public override void OnMount()
        {
            Props.Set("text", "Click me!");
        }

        public override void OnRender(Context ctx, Action<Context> children)
        {
            CountingProvider provider = GetProvider<CountingProvider>();
            
            ctx._<VerticalLayout>(
                props: new Prop("spacing", 10),
                render: ctx =>
                {
                    ctx._<MenuButton>(
                        props: new IProp[]
                        {
                            new EventProp("onClick", () => Props.Set("text", $"Clicked {++_clicks} times")),
                            new Prop("text", Props.Get("text"))
                        }
                    );
                    ctx._<MenuButton>(
                        props: new Prop("text", $"Count: {provider.GetCount()}")
                    );
                    ctx._<MenuButton>(
                        props: new IProp[]
                        {
                            new EventProp("onClick", HandleOptions),
                            new Prop("text", "Options")
                        }
                    );
                    ctx._<MenuButton>(
                        props: new IProp[]
                        {
                            new EventProp("onClick", HandleQuit),
                            new Prop("text", "Exit")
                        }
                    );
                }
            );
        }
    }
}