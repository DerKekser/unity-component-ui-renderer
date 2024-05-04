using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.Extension.ResourceManagement;
using Kekser.ComponentSystem.ComponentUI;
using UnityEngine;
using UnityEngine.UIElements;
using Box = Kekser.ComponentSystem.ComponentUI.Components.Box;

namespace Examples.Todo.Components
{
    public class Layout: UIComponent
    {
        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            ResourceProvider<VisualElement> provider = GetProvider<ResourceProvider<VisualElement>>();
            
            ctx._<Box, StyleProps>(
                props: new StyleProps() { style = new Style()
                {
                    position = new StyleEnum<Position>(Position.Absolute),
                    width = new StyleLength(500),
                    height = new StyleLength(500),
                    paddingBottom = new StyleLength(20),
                    paddingLeft = new StyleLength(20),
                    paddingRight = new StyleLength(20),
                    paddingTop = new StyleLength(20),
                    maxWidth = new StyleLength(Length.Percent(90)),
                    maxHeight = new StyleLength(Length.Percent(90)),
                    top = new StyleLength(Length.Percent(50)),
                    left = new StyleLength(Length.Percent(50)),
                    translate = new StyleTranslate(new Translate(Length.Percent(-50), Length.Percent(-50), 0)),
                    backgroundImage = new StyleBackground(provider.GetResource<Sprite>("Kenny UI/Spritesheet/greySheet/grey_panel.png")),
                    unityFont = new StyleFont(provider.GetResource<Font>("Kenny UI/Font/kenvector_future")),
                }},
                render: Children
            );
        }
    }
}