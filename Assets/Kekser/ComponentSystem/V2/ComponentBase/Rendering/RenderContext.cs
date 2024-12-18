using Kekser.ComponentSystem.V2.ComponentBase.Core;

namespace Kekser.ComponentSystem.V2.ComponentBase.Rendering
{
    public class RenderContext
    {
        private IFragmentContext _context;
        private RenderContext _parent;
        private RenderContext[] _children;
        
        public bool HasChanged(IFragmentContext context)
        {
            // Check if context has changed
            return false;
        }
        
        public bool NeedsRemount(IFragmentContext context)
        {
            // Check if context needs remount
            return false;
        }
        
        public RenderContext FindChild(IFragmentContext context)
        {
            // Find child by context
            return null;
        }
        
        public RenderContext AddChild(RenderContext child)
        {
            // Add child
            return null;
        }
        
        public void RemoveChild(RenderContext child)
        {
            // Remove child
        }
    }
}