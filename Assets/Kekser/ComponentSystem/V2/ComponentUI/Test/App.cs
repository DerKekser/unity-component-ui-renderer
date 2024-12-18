using Kekser.ComponentSystem.ComponentUI.UIProps;
using Kekser.ComponentSystem.V2.ComponentBase;
using Kekser.ComponentSystem.V2.ComponentBase.Components;
using Kekser.ComponentSystem.V2.ComponentBase.Core;
using Kekser.ComponentSystem.V2.ComponentUI.Components;

namespace Kekser.ComponentSystem.V2.ComponentUI.Test
{
    public class App : UIComponent
    {
        protected override IFragmentContext Render()
        {
            bool condition = true;
            int[] numbers = {1, 2, 3, 4, 5};

            return _<Fragment>()._(
                _<TestComponent>(),
                _<Group, StyleProps>(
                    props: new StyleProps()
                    {
                        className = "test"
                    }
                )._(
                    _<TestComponent>(),
                    condition ? _<TestComponent>() : null,
                    _("Hello, World!"),
                    _(123.ToString()),
                    _(numbers, (number, index) => {
                        return _<TestComponent>(key: index)._(
                            _(number.ToString())
                        );
                    }),
                    _(() => {
                        return _<TestComponent>();
                    }),
                    _(() => {
                        return null;
                    })
                )
            );
        }
    }
}