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
            _renderer.Render(ctx => ctx._<ResourceProvider<VisualElement>, ResourceProps>(
                props: new () { resources = _resourceDatabase },
                render: ctx => ctx._<App, AppProps>(
                    props: new () { style = new Style() { height = new StyleLength(Length.Percent(100)) } }    
                )
            ), _uiDocument.rootVisualElement);
        }
        
        private void Update()
        {
            _renderer.Update();
        }
    }
}