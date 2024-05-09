using Kekser.ComponentSystem.ComponentBase;
using UnityEngine.UIElements;
using Box = Kekser.ComponentSystem.ComponentUI.Components.Box;

namespace Kekser.ComponentSystem.ComponentUI
{
    public abstract class UIPortalProps: PortalProps<VisualElement>
    {
        
    }
    
    public abstract class UIPortal<TPortalProps>: BasePortal<VisualElement, TPortalProps> where TPortalProps: UIPortalProps, new()
    {
        
    }
}