using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine;
using UnityEngine.UIElements;
using TextField = Kekser.ComponentSystem.ComponentUI.Components.TextField;

namespace Examples.Todo.Components
{
    public class StyledInputProps: StyleProps
    {
        public OptionalValue<string> value { get; set; } = new();
        public OptionalValue<Action<string>> onChange { get; set; } = new();
    }
    
    public class StyledInput: UIComponent<StyledInputProps>
    {
        public override void OnRender()
        {
            _<TextField, TextFieldProps>(
                props: new TextFieldProps()
                {
                    onChange = OwnProps.onChange,
                    value = OwnProps.value,
                    className = "bg-[Assets/Kenny%20UI/Spritesheet/greySheet.png@grey_button13.png] w-[100%] h-[100%] font-24 color-black [&>TextInput]:color-black [&>TextInput]:p-10",
                }
            );
        }
    }
}