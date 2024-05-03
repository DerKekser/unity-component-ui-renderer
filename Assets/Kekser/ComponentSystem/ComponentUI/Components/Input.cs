using System;
using Kekser.ComponentSystem.ComponentBase;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class Input: UIComponent<TextField>
    {
        private void Change(ChangeEvent<string> eChangeEvent)
        {
            Action<string> e = Props.Get<Action<string>>("onChange");
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
                FragmentNode.value = Props.Get<string>("value");
        }
    }
}