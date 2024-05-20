using Kekser.ComponentSystem.ComponentBase;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI
{
    public class UIContextProvider: BaseContextProvider<VisualElement>
    {
        
    }
    
    public class UIContextProvider<TProps>: BaseContextProvider<VisualElement, TProps> where TProps : class, new()
    {
        
    }
}