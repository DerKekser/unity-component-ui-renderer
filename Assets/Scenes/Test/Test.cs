using Scenes.Kekser.ComponentUI;
using UnityEngine;

namespace Scenes.Test
{
    public class Test : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;
        
        private UIRenderer _renderer;
        
        private void Start()
        {
            _renderer = new UIRenderer();
            _renderer.Render(ctx => ctx._<App>(), _canvas);
        }
        
        private void Update()
        {
            _renderer.Update();
        }
    }
}