using System;
using Kekser.ComponentSystem.ComponentBase;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class ScrollArea: UIComponent<ScrollView>
    {
        private ScrollView _scrollView;
        
        public override void Mount(VisualElement parent)
        {
            base.Mount(parent);
            _scrollView = Node;
            _node = _scrollView.contentContainer;
        }
        
        public override void Unmount()
        {
            _node = _scrollView;
            base.Unmount();
        }
        
        public override void OnRender(BaseContext<VisualElement> ctx, Action<BaseContext<VisualElement>> children)
        {
            children?.Invoke(ctx);
        }
    }
}