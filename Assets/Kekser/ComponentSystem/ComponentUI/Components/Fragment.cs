namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public sealed class Fragment: UIFragment
    {
        protected override void OnRender()
        {
            Children();
        }
    }
}