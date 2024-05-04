using Kekser.ComponentSystem.ComponentBase;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI
{
    public abstract class UIFragment : BaseFragment<VisualElement>
    {
        
    }   
    
    public abstract class UIFragment<TProps> : BaseFragment<VisualElement, TProps> where TProps : struct
    {
        
    }
}