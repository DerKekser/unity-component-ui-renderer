using System.Linq;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;

namespace Examples.AllComponents.Pages
{
    public class ScrollViewPage: UIComponent
    {
        protected override void OnRender()
        {
            _<ScrollView>(
                render: () =>
                {
                    Each(Enumerable.Range(0, 100), (i, _) =>
                    {
                        _<Label, LabelProps>(props: new LabelProps() { text = $"Item {i}" });
                    });
                }
            );
        }
    }
}