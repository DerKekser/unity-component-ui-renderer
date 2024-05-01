﻿using Kekser.ComponentUI;
using Kekser.ComponentUI.Extension.ResourceManagement;
using Kekser.ComponentUI.PropSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Example
{
    public class Example : MonoBehaviour
    {
        [SerializeField] 
        private UIDocument _uiDocument;
        [SerializeField]
        private ResourceDatabase _resourceDatabase;
        
        private UIRenderer _renderer;
        
        private void Start()
        {
            _renderer = new UIRenderer();
            _renderer.Render(ctx => ctx._<ResourceProvider>(
                props: new Prop("resources", _resourceDatabase), 
                render: ctx => ctx._<App>()
            ), _uiDocument);
        }
        
        private void Update()
        {
            _renderer.Update();
        }
    }
}