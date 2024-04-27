using System;

namespace Scenes.Kekser.ComponentUI.Components
{
    public class VerticalLayout: UIComponent
    {
        private UnityEngine.UI.VerticalLayoutGroup _layoutGroup;

        public override void OnMount()
        {
            _layoutGroup = Node.gameObject.AddComponent<UnityEngine.UI.VerticalLayoutGroup>();
            _layoutGroup.childControlWidth = Props.Get("childControlWidth", true);
            _layoutGroup.childControlHeight = Props.Get("childControlHeight", true);
            _layoutGroup.childForceExpandWidth = Props.Get("childForceExpandWidth", true);
            _layoutGroup.childForceExpandHeight = Props.Get("childForceExpandHeight", true);
        }

        public override void OnRender(Context ctx, Action<Context> children)
        {
            children?.Invoke(ctx);
        }
    }
}