using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine;
using UnityEngine.UIElements;
using Box = Kekser.ComponentSystem.ComponentUI.Components.Box;

namespace Examples.AllComponents.Components
{
    public class Layout: UIComponent
    {
        protected override void OnRender()
        {
            _<Box, StyleProps>(
                props: new StyleProps() { 
                    className = "absolute flex-row top-[50%] left-[50%] max-w-[90%] max-h-[90%] p-20 bg-[#cccccc] rounded-10 translate-[-50%_-50%]",
                },
                render: Children
            );
        }
    }
}