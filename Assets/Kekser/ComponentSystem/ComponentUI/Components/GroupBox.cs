using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class GroupBoxProps: StyleProps
    {
        public OptionalValue<string> text { get; set; } = new();
    }
    
    public sealed class GroupBox: UIComponent<UnityEngine.UIElements.GroupBox, GroupBoxProps>
    {
        public override void OnRender()
        {
            if (OwnProps.text.IsSet)
                FragmentRoot.text = OwnProps.text;
            
            Children();
        }
    }
}