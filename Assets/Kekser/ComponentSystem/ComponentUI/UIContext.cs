using Kekser.ComponentSystem.ComponentBase;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI
{
    public class UIContext: BaseContext<VisualElement>
    {
        public UIContext(VisualElement mainNode) : base(mainNode)
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
    }
}