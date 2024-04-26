using Scenes.Kekser.ComponentUI;
using UnityEngine;
using Canvas = Scenes.Kekser.ComponentUI.Components.Canvas;

namespace Scenes.Test
{
    public class Test : MonoBehaviour
    {
        private UIRenderer _renderer;
        
        private void Start()
        {
            _renderer = new UIRenderer();
            _renderer.Render(ctx => ctx._<Canvas>(render: ctx => ctx._<Menu>()));
        }
        
        private void Update()
        {
            _renderer.Update();
        }
    }
}