using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class ToggleProps: StyleProps
    {
        public OptionalValue<bool> value { get; set; } = new();
        public OptionalValue<System.Action<bool>> onChange { get; set; } = new();
    }
    
    public sealed class Toggle: UIComponent<UnityEngine.UIElements.Toggle, ToggleProps>
    {
        private void Change(UnityEngine.UIElements.ChangeEvent<bool> e)
        {
            System.Action<bool> eAction = OwnProps.onChange;
            eAction?.Invoke(e.newValue);
        }

        public override void OnMount()
        {
            FragmentRoot.RegisterValueChangedCallback(Change);
        }
        
        public override void OnUnmount()
        {
            FragmentRoot.RegisterValueChangedCallback(Change);
        }

        public override void OnRender()
        {
            if (OwnProps.value.IsSet)
                FragmentRoot.value = OwnProps.value;
            
            Children();
        }
    }
}