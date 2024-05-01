using System;

namespace Kekser.ComponentUI.Components
{
    public sealed class Text: UIComponent<UnityEngine.UIElements.Label>
    {
        public override void OnRender(Context ctx, Action<Context> children)
        {
            Node.text = Props.Get("text", "");
        }
    }
}