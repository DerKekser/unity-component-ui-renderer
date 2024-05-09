using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class ButtonProps: StyleProps
    {
        public OptionalValue<Action> onClick { get; set; } = new();
    }
    
    public sealed class Button : UIComponent<UnityEngine.UIElements.Button, ButtonProps>
    {
        private void Click()
        {
            Action e = OwnProps.onClick;
            e?.Invoke();
        }
        
        public override void OnMount()
        {
            FragmentRoot.clickable.clicked += Click;
            FragmentRoot.text = "";
        }
        
        public override void OnUnmount()
        {
            FragmentRoot.clickable.clicked -= Click;
        }
        
        public override void OnRender()
        {
            Children();
        }
    }
}