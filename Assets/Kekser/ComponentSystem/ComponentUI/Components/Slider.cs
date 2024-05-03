using System;
using Kekser.ComponentSystem.ComponentBase;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class Slider: UIComponent<UnityEngine.UIElements.Slider>
    {
        private void Change(ChangeEvent<float> eChangeEvent)
        {
            Action<float> e = Props.Get<Action<float>>("onChange");
            e?.Invoke(eChangeEvent.newValue);
        }
        
        public override void OnMount()
        {
            FragmentNode.RegisterValueChangedCallback(Change);
        }
        
        public override void OnUnmount()
        {
            FragmentNode.UnregisterValueChangedCallback(Change);
        }
        
        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            if (Props.Has("value"))
                FragmentNode.value = Props.Get<float>("value");
        }
    }
}