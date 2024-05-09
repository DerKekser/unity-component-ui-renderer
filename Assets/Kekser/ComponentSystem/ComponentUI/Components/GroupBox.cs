using Kekser.ComponentSystem.ComponentUI.UIProps;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class GroupBox: UIComponent<UnityEngine.UIElements.GroupBox, StyleProps>
    {
        public override void OnRender()
        {
            Children();
        }
    }
}