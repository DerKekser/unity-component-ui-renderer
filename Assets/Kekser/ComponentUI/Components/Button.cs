using System;

namespace Kekser.ComponentUI.Components
{
    public sealed class Button : UIComponent<UnityEngine.UIElements.Button>
    {
        private void Click()
        {
            Action e = Props.Get<Action>("onClick");
            e?.Invoke();
        }
        
        public override void OnMount()
        {
            Node.clickable.clicked += Click;
            Node.text = "";
        }
        
        public override void OnUnmount()
        {
            Node.clickable.clicked -= Click;
        }
        
        public override void OnRender(Context ctx, Action<Context> children)
        {
            children?.Invoke(ctx);
        }
    }
}