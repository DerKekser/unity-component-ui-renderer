using Kekser.ComponentSystem.ComponentBase;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI
{
    public abstract class UIProvider: UIProvider<NoProps>
    {
        
    }
    
    public abstract class UIProvider<TProps>: BaseProvider<VisualElement, TProps> where TProps : struct
    {
        
    }
}