using System;

namespace Scenes.Kekser.ComponentUI.Components
{
    public sealed class VerticalLayout: UIComponent
    {
        private UnityEngine.UI.VerticalLayoutGroup _layoutGroup;

        public override void OnMount()
        {
            _layoutGroup = Node.gameObject.AddComponent<UnityEngine.UI.VerticalLayoutGroup>();
        }

        public override void OnRender(Context ctx, Action<Context> children)
        {
            _layoutGroup.childControlWidth = Props.Get("childControlWidth", true);
            _layoutGroup.childControlHeight = Props.Get("childControlHeight", true);
            _layoutGroup.childForceExpandWidth = Props.Get("childForceExpandWidth", true);
            _layoutGroup.childForceExpandHeight = Props.Get("childForceExpandHeight", true);
            _layoutGroup.spacing = Props.Get("spacing", 0);
            _layoutGroup.padding = Props.Get("padding", new UnityEngine.RectOffset(0, 0, 0, 0));
            _layoutGroup.childAlignment = Props.Get("childAlignment", UnityEngine.TextAnchor.UpperLeft);
            
            children?.Invoke(ctx);
        }
    }
}