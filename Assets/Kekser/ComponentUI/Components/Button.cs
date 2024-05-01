using System;
using UnityEngine;

namespace Kekser.ComponentUI.Components
{
    public sealed class Button : UIComponent
    {
        private UnityEngine.UI.Button _button;
        
        private void Click()
        {
            Action e = Props.Get<Action>("onClick");
            e?.Invoke();
        }
        
        public override void OnMount()
        {
            _button = Node.gameObject.AddComponent<UnityEngine.UI.Button>();
            _button.onClick.AddListener(Click);
        }
        
        public override void OnUnmount()
        {
            _button.onClick.RemoveListener(Click);
        }
        
        public override void OnRender(Context ctx, Action<Context> children)
        {
            _button.interactable = Props.Get("interactable", true);
            
            children?.Invoke(ctx);
        }
    }
}