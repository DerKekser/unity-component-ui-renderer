using Kekser.ComponentSystem.V2.ComponentBase.Core;

namespace Kekser.ComponentSystem.V2.ComponentBase.Rendering
{
    public class FragmentRenderer
    {
        private IFragment _fragment;
        private RenderContext _renderContext;
        
        public FragmentRenderer(IFragment fragment)
        {
            _fragment = fragment;
        }
        
        public void Tick()
        {
            IFragmentContext fragmentContext = _fragment.GetContext();
            UpdateContext(fragmentContext, _renderContext);
        }

        private RenderContext UpdateContext(IFragmentContext fragmentContext, RenderContext renderContext)
        {
            if (fragmentContext == null && renderContext == null)
                return null;

            if (fragmentContext == null)
            {
                // Remove render context
                // Unmount fragment
                return null;
            }
            
            if (renderContext == null)
            {
                // Create render context
                // Mount fragment
                return renderContext;
            }

            if (renderContext.HasChanged(fragmentContext))
            {
                if (renderContext.NeedsRemount(fragmentContext))
                {
                    // Unmount fragment
                    // Mount fragment
                }
                else
                {
                    // Update fragment
                }
            }
            
            // Update children
            for (int i = 0; i < fragmentContext.Children.Length; i++)
            {
                IFragmentContext childFragmentContext = fragmentContext.Children[i];
                RenderContext childRenderContext = renderContext.FindChild(childFragmentContext);
                RenderContext newChildRenderContext = UpdateContext(childFragmentContext, childRenderContext);
                
                if (newChildRenderContext != null)
                {
                    if (childRenderContext == null)
                    {
                        renderContext.AddChild(newChildRenderContext);
                    }
                    else if (newChildRenderContext != childRenderContext)
                    {
                        renderContext.RemoveChild(childRenderContext);
                        renderContext.AddChild(newChildRenderContext);
                    }
                }
                else
                {
                    if (childRenderContext != null)
                    {
                        renderContext.RemoveChild(childRenderContext);
                    }
                }
            }
            
            return renderContext;
        }
    }
}