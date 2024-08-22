using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.Scripting;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class LabelProps: StyleProps
    {
        public OptionalValue<string> text { get; set; } = new();
    }
    
    [Preserve]
    public sealed class Label: UIComponent<UnityEngine.UIElements.Label, LabelProps>
    {
        protected override void OnRender()
        {
            FragmentRoot.text = Props.text;
        }
    }
}