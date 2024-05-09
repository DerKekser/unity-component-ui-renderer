using Kekser.ComponentSystem.ComponentUI.UIProps;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public sealed class Foldout: UIComponent<UnityEngine.UIElements.Foldout, StyleProps>
    {
        public override void OnRender()
        {
            Children();
        }
    }
}