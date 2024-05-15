namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class Group: UIComponent
    {
        protected override void OnRender()
        {
            Children();
        }
    }
}