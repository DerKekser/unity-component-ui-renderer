using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using Kekser.ComponentSystem.V2.ComponentBase;

namespace Kekser.ComponentSystem.V2.ComponentUI.Components
{
    public class LabelProps: StyleProps
    {
        public OptionalValue<string> text { get; set; } = new();
    }
    
    public class Label : UIComponent<UnityEngine.UIElements.Label, LabelProps>
    {
        
    }
}