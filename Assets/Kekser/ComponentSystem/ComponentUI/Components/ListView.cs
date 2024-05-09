using Kekser.ComponentSystem.ComponentUI.UIProps;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public sealed class ListView: UIComponent<UnityEngine.UIElements.ListView, StyleProps>
    {
        public override void OnRender()
        {
            Children();
        }
    }
}