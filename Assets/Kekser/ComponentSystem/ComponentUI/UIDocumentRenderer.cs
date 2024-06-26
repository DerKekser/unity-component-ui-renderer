﻿using Kekser.ComponentSystem.ComponentBase;
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
        
        [SerializeField]
        private bool _log = false;
        
        private UIRenderer _renderer;
        
        private void CreateRenderer()
        {
            if (_uiDocument == null)
                return;
#if UNITY_EDITOR
            _uiDocument.runInEditMode = true;
#endif
            _uiDocument.rootVisualElement.styleSheets.Clear();
            foreach (var style in _styles)
                _uiDocument.rootVisualElement.styleSheets.Add(style);

            _renderer = new UIRenderer
            {
                Logging = _log
            };
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
#if UNITY_EDITOR
            if (_renderer == null)
                CreateRenderer();
#endif
            _renderer?.Update();
        }
        
        private void OnDestroy()
        {
            _renderer?.Destroy();
        }
    }
}