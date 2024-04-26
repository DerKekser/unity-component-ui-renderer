using System;
using UnityEngine;

namespace Scenes.Kekser.ComponentUI.Components
{
    public class Canvas : UIComponent
    {
        private UnityEngine.Canvas _canvas;
        
        public override void OnMount()
        {
            _canvas = Node.gameObject.AddComponent<UnityEngine.Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _canvas.gameObject.AddComponent<UnityEngine.UI.GraphicRaycaster>();
        }
        
        public override void OnRender(Context ctx, Action<Context> children)
        {
            children?.Invoke(ctx);
        }
    }
}