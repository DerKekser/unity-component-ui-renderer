using System;
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
    }
}