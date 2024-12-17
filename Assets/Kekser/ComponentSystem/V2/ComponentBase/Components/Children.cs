using UnityEngine.Scripting;

namespace Kekser.ComponentSystem.V2.ComponentBase.Components
{
    [Preserve]
    public sealed class Children : BaseFragment
    {
        protected override IFragmentContext Render()
        {
            return new ChildrenTargetFragmentContext(null);
        }
    }
}