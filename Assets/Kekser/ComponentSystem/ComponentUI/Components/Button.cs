using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class ButtonProps: StyleProps
    {
        public OptionalValue<string> text { get; set; } = new();
        public OptionalValue<Action> onClick { get; set; } = new();
        public OptionalValue<Action> onPointerDown { get; set; } = new();
        public OptionalValue<Action> onPointerUp { get; set; } = new();
    }
    
    [Preserve]
    public sealed class Button : UIComponent<UnityEngine.UIElements.Button, ButtonProps>
    {
        private void Click()
        {
            Action e = Props.onClick;
            e?.Invoke();
        }
        
        // TODO: Add onPointerDown and onPointerUp events on every component
        private void OnPointerDown(PointerDownEvent pEvent)
        {
            Action e = Props.onPointerDown;
            e?.Invoke();
        }
        
        private void OnPointerUp(PointerUpEvent pEvent)
        {
            Action e = Props.onPointerUp;
            e?.Invoke();
        }

        protected override void OnMount()
        {
            FragmentRoot.clickable.clicked += Click;
            FragmentRoot.RegisterCallback<PointerDownEvent>(OnPointerDown, TrickleDown.TrickleDown);
            FragmentRoot.RegisterCallback<PointerUpEvent>(OnPointerUp, TrickleDown.TrickleDown);
            FragmentRoot.text = "";
        }

        protected override void OnUnmount()
        {
            FragmentRoot.clickable.clicked -= Click;
            FragmentRoot.UnregisterCallback<PointerDownEvent>(OnPointerDown);
            FragmentRoot.UnregisterCallback<PointerUpEvent>(OnPointerUp);
        }

        protected override void OnRender()
        {
            if (Props.text.IsSet)
                FragmentRoot.text = Props.text;
            
            Children();
        }
    }
}