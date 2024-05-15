using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class FoldoutProps: StyleProps
    {
        public OptionalValue<string> text { get; set; } = new();
        public OptionalValue<bool> value { get; set; } = new();
        public OptionalValue<Action<bool>> onChange { get; set; } = new();
    }
    
    public sealed class Foldout: UIComponent<UnityEngine.UIElements.Foldout, FoldoutProps>
    {
        private void Change(ChangeEvent<bool> e)
        {
            Action<bool> eAction = Props.onChange;
            eAction?.Invoke(e.newValue);
        }

        protected override void OnMount()
        {
            FragmentRoot.RegisterValueChangedCallback(Change);
        }

        protected override void OnUnmount()
        {
            FragmentRoot.UnregisterValueChangedCallback(Change);
        }

        protected override void OnRender()
        {
            if (Props.text.IsSet)
                FragmentRoot.text = Props.text;
            if (Props.value.IsSet)
                FragmentRoot.value = Props.value;
            
            Children();
        }
    }
}