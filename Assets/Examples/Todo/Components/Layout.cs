using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.UIElements;

namespace Examples.Todo.Components
{
    public class Layout: UIComponent
    {
        public override void OnRender()
        {
            _<Group, StyleProps>(
                props: new StyleProps() { 
                    className = "todo-bg absolute p-20 w-500 h-500 max-w-[90%] max-h-[90%] t-[50%] l-[50%] translate-[-50%]",
                },
                render: Children
            );
        }
    }
}