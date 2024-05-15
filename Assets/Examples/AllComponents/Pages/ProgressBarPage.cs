using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;

namespace Examples.AllComponents.Pages
{
    public class ProgressBarPage: UIComponent
    {
        protected override void OnRender()
        {
            _<ProgressBar, ProgressBarProps>(
                props: new ProgressBarProps()
                {
                    value = 50f,
                    highValue = 100f,
                    lowValue = 0f
                }
            );
        }
    }
}