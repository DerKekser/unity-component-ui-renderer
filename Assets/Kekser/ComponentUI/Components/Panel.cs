using System;
using UnityEngine;

namespace Kekser.ComponentUI.Components
{
    public sealed class Panel: UIComponent
    {
        private UnityEngine.UI.Image _image;
        
        public override void OnMount()
        {
            _image = Node.gameObject.AddComponent<UnityEngine.UI.Image>();
        }

        public override void OnRender(Context ctx, Action<Context> children)
        {
            _image.color = Props.Get("color", Color.white);
            _image.sprite = Props.Get<Sprite>("sprite", null);
            _image.type = Props.Get("spriteType", UnityEngine.UI.Image.Type.Simple);
            
            children?.Invoke(ctx);
        }
    }
}