using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.Scripting;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    [Preserve]
    public sealed class Box : UIComponent<UnityEngine.UIElements.Box, StyleProps>
    {
        protected override void OnRender()
        {
            Children();
        }
    }
}