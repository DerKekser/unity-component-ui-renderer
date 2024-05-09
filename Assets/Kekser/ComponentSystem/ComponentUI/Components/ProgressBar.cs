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
        public override void OnRender()
        {
            if (OwnProps.value.IsSet)
                FragmentRoot.value = OwnProps.value;
            if (OwnProps.highValue.IsSet)
                FragmentRoot.highValue = OwnProps.highValue;
            if (OwnProps.lowValue.IsSet)
                FragmentRoot.lowValue = OwnProps.lowValue;
            
            Children();
        }
    }
}