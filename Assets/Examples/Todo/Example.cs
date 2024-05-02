using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Extension.ResourceManagement;
using UnityEngine;
using UnityEngine.UIElements;

namespace Examples.Todo
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
                render: ctx => ctx._<App>(props: new Prop("height", new StyleLength(Length.Percent(100))))
            ), _uiDocument.rootVisualElement);
        }
        
        private void Update()
        {
            _renderer.Update();
        }
    }
}