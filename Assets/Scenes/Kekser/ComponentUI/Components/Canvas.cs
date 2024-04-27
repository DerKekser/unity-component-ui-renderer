using System;
using UnityEngine;

namespace Scenes.Kekser.ComponentUI.Components
{
    public sealed class Canvas : UIComponent
    {
        private UnityEngine.Canvas _canvas;
        private UnityEngine.UI.GraphicRaycaster _raycaster;
        private UnityEngine.UI.CanvasScaler _scaler;
        
        public override void OnMount()
        {
            _canvas = Node.gameObject.AddComponent<UnityEngine.Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _raycaster = _canvas.gameObject.AddComponent<UnityEngine.UI.GraphicRaycaster>();
            _scaler = _canvas.gameObject.AddComponent<UnityEngine.UI.CanvasScaler>();
            _scaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
        }
        
        public override void OnRender(Context ctx, Action<Context> children)
        {
            children?.Invoke(ctx);
        }
    }
}