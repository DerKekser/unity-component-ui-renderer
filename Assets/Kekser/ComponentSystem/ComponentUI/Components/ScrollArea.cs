using System;
using Kekser.ComponentSystem.ComponentBase;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class ScrollArea: UIComponent<ScrollView>
    {
        public override void Mount(VisualElement parent)
        {
            base.Mount(parent);
            _fragmentNode = FragmentRoot.contentContainer;
        }
        
        public override void OnRender(BaseContext<VisualElement> ctx, Action<BaseContext<VisualElement>> children)
        {
            children?.Invoke(ctx);
        }
    }
}