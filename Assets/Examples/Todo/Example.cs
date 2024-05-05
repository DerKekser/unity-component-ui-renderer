using Kekser.ComponentSystem.ComponentBase.Extension.ResourceManagement;
using Kekser.ComponentSystem.ComponentUI;
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
            _renderer.Render(ctx => ctx.CreateComponent<ResourceProvider<VisualElement>, ResourceProps>(
                props: new () { resources = _resourceDatabase },
                render: ctx => ctx.CreateComponent<App>()
            ), _uiDocument.rootVisualElement);
        }
        
        private void Update()
        {
            _renderer.Update();
        }
    }
}