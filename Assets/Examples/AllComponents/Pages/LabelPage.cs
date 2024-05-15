using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;

namespace Examples.AllComponents.Pages
{
    public class LabelPage: UIComponent
    {
        protected override void OnRender()
        {
            _<Label, LabelProps>(props: new LabelProps() { text = "Label" });
        }
    }
}