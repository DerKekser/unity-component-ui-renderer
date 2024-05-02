using Kekser.ComponentSystem.ComponentBase;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI
{
    public class UIContextHolder: BaseContextHolder<VisualElement>
    {
        public UIContextHolder(BaseContext<VisualElement> context) : base(context)
        {
        }

        protected override BaseContext<VisualElement> CreateContext(BaseContext<VisualElement> parent)
        {
            return new UIContext(parent);
        }
    }
}