using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.UIElements;

namespace Examples.Todo.Components
{
    public class Layout: UIComponent
    {
        protected override void OnRender()
        {
            _<Group, StyleProps>(
                props: new StyleProps() { 
                    className = "bg-[/Assets/Kenny%20UI/Spritesheet/greySheet.png#grey_panel.png] unity-font-[/Assets/Kenny%20UI/Font/kenvector_future.ttf] absolute p-20 w-500 h-500 max-w-[90%] max-h-[90%] top-[50%] left-[50%] translate-[-50%_-50%]",
                },
                render: Children
            );
        }
    }
}