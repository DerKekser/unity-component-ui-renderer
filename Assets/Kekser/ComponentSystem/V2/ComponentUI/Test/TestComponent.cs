using Kekser.ComponentSystem.V2.ComponentBase;
using Kekser.ComponentSystem.V2.ComponentBase.Components;
using Kekser.ComponentSystem.V2.ComponentBase.Core;

namespace Kekser.ComponentSystem.V2.ComponentUI.Test
{
    public class TestComponent : UIComponent
    {
        protected override IFragmentContext Render()
        {
            return _<Fragment>()._(
                _("Test Component"),
                _<Children>()
            );
        }
    }
}