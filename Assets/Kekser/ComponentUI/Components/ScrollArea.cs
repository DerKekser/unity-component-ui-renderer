using System;

namespace Kekser.ComponentUI.Components
{
    public class ScrollArea: UIComponent<UnityEngine.UIElements.ScrollView>
    {
        private UnityEngine.UIElements.ScrollView _scrollView;
        
        public override void Mount(UnityEngine.UIElements.VisualElement parent)
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
        
        public override void OnRender(Context ctx, Action<Context> children)
        {
            children?.Invoke(ctx);
        }
    }
}