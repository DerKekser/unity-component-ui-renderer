using Kekser.ComponentUI.PropSystem;
using UnityEngine.UI;

namespace Kekser.ComponentUI.Components
{
    public sealed class ScrollArea: UIComponent
    {
        private sealed class ScrollBarHorizontal: UIComponent
        {
        
        }
    
        private sealed class ScrollBarVertical: UIComponent
        {
            
        }
        
        private sealed class Viewport: UIComponent
        {
            private Image _image;
            private Mask _mask;
            
            public override void OnMount()
            {
                _image = Node.gameObject.AddComponent<Image>();
                _mask = Node.gameObject.AddComponent<Mask>();
                _mask.showMaskGraphic = false;
            }
            
            public override void OnRender(Context ctx, System.Action<Context> children)
            {
                children?.Invoke(ctx);
            }
        }
        
        private sealed class Content: UIComponent
        {
            private ContentSizeFitter _contentSizeFitter;

            public override void OnMount()
            {
                /*_contentSizeFitter = Node.gameObject.AddComponent<ContentSizeFitter>();
                _contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                _contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;*/
            }

            public override void OnRender(Context ctx, System.Action<Context> children)
            {
                children?.Invoke(ctx);
            }
        }
        
        private ScrollRect _scrollRect;
        
        public override void OnMount()
        {
            _scrollRect = Node.gameObject.AddComponent<ScrollRect>();
        }

        public override void OnRender(Context ctx, System.Action<Context> children)
        {
            _scrollRect.horizontal = Props.Get("horizontal", true);
            _scrollRect.vertical = Props.Get("vertical", true);
            _scrollRect.movementType = Props.Get("movementType", ScrollRect.MovementType.Elastic);
            _scrollRect.elasticity = Props.Get("elasticity", 0.1f);
            _scrollRect.inertia = Props.Get("inertia", true);
            _scrollRect.decelerationRate = Props.Get("decelerationRate", 0.135f);
            _scrollRect.scrollSensitivity = Props.Get("scrollSensitivity", 1);
            
            _scrollRect.viewport = ctx._<Viewport>(
                render: ctx =>
                {
                    _scrollRect.content = ctx._<VerticalLayout>(
                        props: new IProp[]
                        {
                            new Prop("childControlHeight", false),
                            new Prop("childForceExpandHeight", false),
                        },
                        render: ctx =>
                        {
                            children?.Invoke(ctx);
                        }
                    ).Node;
                }
            ).Node;
            ctx._<ScrollBarHorizontal>();
            ctx._<ScrollBarVertical>();
        }
    }
}