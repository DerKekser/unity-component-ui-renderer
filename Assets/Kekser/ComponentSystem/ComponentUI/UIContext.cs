using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentUI.Components;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI
{
    public class UIContext: BaseContext<VisualElement>
    {
        public UIContext(IFragment<VisualElement> fragment) : base(fragment)
        {
        }

        public UIContext(BaseContext<VisualElement> parent) : base(parent)
        {
        }

        protected override BaseContextHolder<VisualElement> CreateContextHolder(BaseContext<VisualElement> context)
        {
            return new UIContextHolder(context);
        }

        protected override void SetNodeAsLastSibling(VisualElement node)
        {
            node?.BringToFront();
        }

        public Text _(string text) => _<Text, TextProps>(props: new TextProps() {text = text});
    }
}