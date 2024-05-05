using Kekser.ComponentSystem.ComponentBase.PropSystem;

namespace Kekser.ComponentSystem.ComponentUI.UIProps
{
    public class StyleProps: IStyleProp, IClassNameProp
    {
        public OptionalValue<Style> style { get; set; } = new();
        public OptionalValue<string> className { get; set; } = new();
    }
}