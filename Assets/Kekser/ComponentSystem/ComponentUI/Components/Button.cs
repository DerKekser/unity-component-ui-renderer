using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class ButtonProps: StyleProps
    {
        public OptionalValue<string> text { get; set; } = new();
        public OptionalValue<Action> onClick { get; set; } = new();
    }
    
    public sealed class Button : UIComponent<UnityEngine.UIElements.Button, ButtonProps>
    {
        private void Click()
        {
            Action e = Props.onClick;
            e?.Invoke();
        }

        protected override void OnMount()
        {
            FragmentRoot.clickable.clicked += Click;
            FragmentRoot.text = "";
        }

        protected override void OnUnmount()
        {
            FragmentRoot.clickable.clicked -= Click;
        }

        protected override void OnRender()
        {
            if (Props.text.IsSet)
                FragmentRoot.text = Props.text;
            
            Children();
        }
    }
}