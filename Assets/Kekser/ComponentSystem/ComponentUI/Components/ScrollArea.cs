using Kekser.ComponentSystem.ComponentBase;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public sealed class ScrollArea: UIComponent<ScrollView, StyleProps>
    {
        public override void Mount(VisualElement parent)
        {
            base.Mount(parent);
            _fragmentNode = FragmentRoot.contentContainer;
        }
        
        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            Children(ctx);
        }
    }
}