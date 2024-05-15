using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;

namespace Examples.AllComponents.Pages
{
    public class GroupBoxPage: UIComponent
    {
        protected override void OnRender()
        {
            _<GroupBox, GroupBoxProps>(
                props: new GroupBoxProps()
                {
                    text = "GroupBox",
                },
                render: () =>
                {
                    _<Label, LabelProps>(props: new LabelProps() { text = "GroupBox content" });
                }
            );
        }
    }
}