using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.Scripting;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    [Preserve]
    public sealed class ScrollView: UIComponent<UnityEngine.UIElements.ScrollView, StyleProps>
    {
        protected override void OnRender()
        {
            Children();
        }
    }
}