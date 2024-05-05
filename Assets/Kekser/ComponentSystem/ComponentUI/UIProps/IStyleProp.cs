using Kekser.ComponentSystem.ComponentBase.PropSystem;

namespace Kekser.ComponentSystem.ComponentUI.UIProps
{
    public interface IStyleProp
    {
        OptionalValue<Style> style { get; set; }
    }
}