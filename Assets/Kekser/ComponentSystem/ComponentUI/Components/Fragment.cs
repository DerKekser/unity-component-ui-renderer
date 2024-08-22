using UnityEngine.Scripting;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    [Preserve]
    public sealed class Fragment: UIFragment
    {
        protected override void OnRender()
        {
            Children();
        }
    }
}