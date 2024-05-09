using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine;
using UnityEngine.UIElements;
using Box = Kekser.ComponentSystem.ComponentUI.Components.Box;

namespace Examples.AllComponents.Components
{
    public class Layout: UIComponent
    {
        public override void OnRender()
        {
            _<Box, StyleProps>(
                props: new StyleProps() { style = new Style()
                {
                    position = new StyleEnum<Position>(Position.Absolute),
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row),
                    top = new StyleLength(Length.Percent(50)),
                    left = new StyleLength(Length.Percent(50)),
                    maxHeight = new StyleLength(Length.Percent(90)),
                    maxWidth = new StyleLength(Length.Percent(90)),
                    paddingTop = new StyleLength(20),
                    paddingRight = new StyleLength(20),
                    paddingBottom = new StyleLength(20),
                    paddingLeft = new StyleLength(20),
                    backgroundColor = new StyleColor(new Color(0.8f, 0.8f, 0.8f, 1)),
                    borderTopRightRadius = new StyleLength(10),
                    borderBottomRightRadius = new StyleLength(10),
                    borderBottomLeftRadius = new StyleLength(10),
                    borderTopLeftRadius = new StyleLength(10),
                    translate = new StyleTranslate(new Translate(Length.Percent(-50), Length.Percent(-50), 0)),
                }},
                render: Children
            );
        }
    }
}