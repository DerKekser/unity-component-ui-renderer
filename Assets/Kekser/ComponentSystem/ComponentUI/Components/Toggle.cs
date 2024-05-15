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
            System.Action<bool> eAction = Props.onChange;
            eAction?.Invoke(e.newValue);
        }

        protected override void OnMount()
        {
            FragmentRoot.RegisterValueChangedCallback(Change);
        }

        protected override void OnUnmount()
        {
            FragmentRoot.RegisterValueChangedCallback(Change);
        }

        protected override void OnRender()
        {
            if (Props.value.IsSet)
                FragmentRoot.value = Props.value;
            
            Children();
        }
    }
}