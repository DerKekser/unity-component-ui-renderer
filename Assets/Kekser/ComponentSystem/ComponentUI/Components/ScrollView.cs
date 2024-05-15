using Kekser.ComponentSystem.ComponentUI.UIProps;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public sealed class ScrollView: UIComponent<UnityEngine.UIElements.ScrollView, StyleProps>
    {
        protected override void OnRender()
        {
            Children();
        }
    }
}