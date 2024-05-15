using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;

namespace Examples.AllComponents.Pages
{
    public class FoldoutPage: UIComponent
    {
        protected override void OnRender()
        {
            _<Foldout, FoldoutProps>(
                props: new FoldoutProps()
                {
                    text = "Foldout",
                },
                render: () =>
                {
                    _<Label, LabelProps>(props: new LabelProps() { text = "Foldout content" });
                }
            );
        }
    }
}