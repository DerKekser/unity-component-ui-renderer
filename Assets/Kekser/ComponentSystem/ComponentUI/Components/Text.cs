using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class TextProps: StyleProps
    {
        public OptionalValue<string> text { get; set; } = new();
    }
    
    public sealed class Text: UIComponent<Label, TextProps>
    {
        public override void OnRender()
        {
            FragmentNode.text = OwnProps.text;
        }
    }
}