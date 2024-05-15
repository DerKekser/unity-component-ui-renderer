using Kekser.ComponentSystem.ComponentUI.UIProps;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public sealed class Box : UIComponent<UnityEngine.UIElements.Box, StyleProps>
    {
        protected override void OnRender()
        {
            Children();
        }
    }
}