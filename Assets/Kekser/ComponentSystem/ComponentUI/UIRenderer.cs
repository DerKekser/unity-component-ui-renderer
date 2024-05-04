using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI
{
    public struct UIRendererProps
    {
        public OptionalValue<int> screenWidth { get; set; }
        public OptionalValue<int> screenHeight { get; set; }
    }
    
    public sealed class UIRenderer: BaseRenderer<VisualElement>
    {
        public override BaseContext<VisualElement> CreateContext(VisualElement rootNode)
        {
            return new UIContext(new RenderFragment<VisualElement, UIRendererProps>(rootNode));
        }

        protected override void Tick(BaseContext<VisualElement> ctx)
        {
            ctx.Fragment.Props.Set(new UIRendererProps
            {
                screenWidth = Screen.width,
                screenHeight = Screen.height
            });
        }
    }
}