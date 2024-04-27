using Kekser.ComponentUI;
using UnityEngine;

namespace Example
{
    public class Example : MonoBehaviour
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