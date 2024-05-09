using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class LabelProps: StyleProps
    {
        public OptionalValue<string> text { get; set; } = new();
    }
    
    public sealed class Label: UIComponent<UnityEngine.UIElements.Label, LabelProps>
    {
        public override void OnRender()
        {
            FragmentNode.text = OwnProps.text;
        }
    }
}