using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class ProgressBarProps: StyleProps
    {
        public OptionalValue<float> value { get; set; } = new();
        public OptionalValue<float> highValue { get; set; } = new();
        public OptionalValue<float> lowValue { get; set; } = new();
    }
    
    public sealed class ProgressBar: UIComponent<UnityEngine.UIElements.ProgressBar, ProgressBarProps>
    {
        protected override void OnRender()
        {
            if (Props.value.IsSet)
                FragmentRoot.value = Props.value;
            if (Props.highValue.IsSet)
                FragmentRoot.highValue = Props.highValue;
            if (Props.lowValue.IsSet)
                FragmentRoot.lowValue = Props.lowValue;
            
            Children();
        }
    }
}