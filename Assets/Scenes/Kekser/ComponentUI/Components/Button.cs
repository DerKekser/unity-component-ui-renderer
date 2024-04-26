using System;
using UnityEngine;

namespace Scenes.Kekser.ComponentUI.Components
{
    public class Button : UIComponent
    {
        private UnityEngine.UI.Image _image;
        private UnityEngine.UI.Button _button;
        
        private void Click()
        {
            Action e = Props.Get<Action>("onClick");
            e?.Invoke();
        }
        
        public override void OnMount()
        {
            _image = Node.gameObject.AddComponent<UnityEngine.UI.Image>();
            _button = Node.gameObject.AddComponent<UnityEngine.UI.Button>();
            _button.onClick.AddListener(Click);
        }
        
        public override void OnUnmount()
        {
            _button.onClick.RemoveListener(Click);
        }
        
        public override void OnRender(Context ctx, Action<Context> children)
        {
            children?.Invoke(ctx);
        }
    }
}