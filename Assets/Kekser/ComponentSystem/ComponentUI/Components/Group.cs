namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public class Group: UIComponent
    {
        public override void OnRender()
        {
            Children();
        }
    }
}