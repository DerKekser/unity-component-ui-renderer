using Kekser.ComponentSystem.ComponentBase;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI
{
    [ExecuteAlways]
    public abstract class UIDocumentRenderer<TComponent> : MonoBehaviour where TComponent : IFragment<VisualElement>
    {
        [SerializeField] 
        private UIDocument _uiDocument;
        [SerializeField]
        private StyleSheet[] _styles;
        
        private UIRenderer _renderer;
        
        private void CreateRenderer()
        {
            _uiDocument.runInEditMode = true;
            _uiDocument.rootVisualElement.styleSheets.Clear();
            foreach (var style in _styles)
                _uiDocument.rootVisualElement.styleSheets.Add(style);

            _renderer = new UIRenderer();
            _renderer.Render(Render, _uiDocument.rootVisualElement);
        }
        
        protected virtual void Render(BaseContext<VisualElement> ctx)
        {
            ctx.CreateComponent<TComponent>();
        }
        
        private void Start()
        {
            CreateRenderer();
        }
        
        private void Update()
        {
            if (_renderer == null)
                CreateRenderer();
            _renderer.Update();
        }
    }
}