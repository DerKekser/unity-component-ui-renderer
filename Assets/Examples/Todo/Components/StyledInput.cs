using System;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.Extension.ResourceManagement;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using UnityEngine;
using UnityEngine.UIElements;
using Input = Kekser.ComponentSystem.ComponentUI.Components.Input;

namespace Examples.Todo.Components
{
    public struct StyledInputProps
    {
        public OptionalValue<string> value { get; set; }
        public OptionalValue<Action<string>> onChange { get; set; }
        public OptionalValue<Style> style { get; set; }
    }
    
    public class StyledInput: UIComponent<StyledInputProps>
    {
        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            ResourceProvider<VisualElement> provider = GetProvider<ResourceProvider<VisualElement>>();
            
            ctx._<Input, InputProps>(
                props: new InputProps()
                {
                    onChange = OwnProps.onChange,
                    value = OwnProps.value,
                    style = new Style()
                    {
                        width = new StyleLength(Length.Percent(100)),
                        height = new StyleLength(Length.Percent(100)),
                        paddingLeft = new StyleLength(10),
                        paddingRight = new StyleLength(10),
                        fontSize = new StyleLength(24),
                        color = new StyleColor(Color.black),
                        backgroundImage = new StyleBackground(provider.GetResource<Sprite>("Kenny UI/Spritesheet/greySheet/grey_button13.png")),
                    }
                }
            );
        }
    }
}