using System;
using UnityEngine;

namespace Kekser.ComponentUI.Components
{
    public sealed class Button : UIComponent
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
            _image.color = Props.Get("color", Color.white);
            _image.sprite = Props.Get<Sprite>("sprite", null);
            _image.type = Props.Get("spriteType", UnityEngine.UI.Image.Type.Simple);
            _button.interactable = Props.Get("interactable", true);
            
            children?.Invoke(ctx);
        }
    }
}