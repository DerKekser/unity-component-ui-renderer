using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.UIElements;
using Box = Kekser.ComponentSystem.ComponentUI.Components.Box;

namespace Examples.Todo.Components
{
    public class Layout: UIComponent
    {
        public override void OnRender()
        {
            _<Box, StyleProps>(
                props: new StyleProps() { 
                    className = "todo-bg",
                    style = new Style()
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
                    }
                },
                render: Children
            );
        }
    }
}