using System;
using Kekser.ComponentSystem.ComponentUI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Examples.Todo
{
    public class TodoExample : MonoBehaviour
    {
        [SerializeField] 
        private UIDocument _uiDocument;
        [SerializeField]
        private StyleSheet[] _styles;
        
        private UIRenderer _renderer;
        
        private void Start()
        {
            foreach (var style in _styles)
                _uiDocument.rootVisualElement.styleSheets.Add(style);

            _renderer = new UIRenderer();
            _renderer.Render(ctx => ctx.CreateComponent<App>(), _uiDocument.rootVisualElement);
        }
        
        private void Update()
        {
            _renderer.Update();
        }
    }
}