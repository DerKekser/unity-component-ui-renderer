using System;
using Scenes.Kekser.ComponentUI;
using Scenes.Kekser.ComponentUI.Components;
using Scenes.Test.Components;

namespace Scenes.Test.Pages
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
            ctx._<VerticalLayout>(
                props: props => props.Set("spacing", 10),
                render: ctx =>
            {
                ctx._<MenuButton>(
                    props: props =>
                    {
                        props.Set<Action>("onClick", () => Props.Set("text", "Clicked " + ++_clicks + " times"));
                        props.Set("text", Props.Get("text"));
                    }
                );
                ctx._<MenuButton>(
                    props: props =>
                    {
                        props.Set<Action>("onClick", HandleOptions);
                        props.Set("text", "Options");
                    }
                );
                ctx._<MenuButton>(
                    props: props =>
                    {
                        props.Set<Action>("onClick", HandleQuit);
                        props.Set("text", "Exit");
                    }
                );
            });
        }
    }
}