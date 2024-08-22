using UnityEngine.Scripting;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    [Preserve]
    public sealed class Group: UIComponent
    {
        protected override void OnRender()
        {
            Children();
        }
    }
}