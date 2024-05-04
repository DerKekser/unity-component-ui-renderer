﻿using System;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.Extension.ResourceManagement;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.PropSystem.Rework;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using UnityEngine;
using UnityEngine.UIElements;
using Box = Kekser.ComponentSystem.ComponentUI.Components.Box;
using Button = Kekser.ComponentSystem.ComponentUI.Components.Button;

namespace Examples.Todo.Components
{
    public struct TodoEntryProps
    {
        public ObligatoryValue<string> todo { get; set; }
        public ObligatoryValue<Action> onRemove { get; set; }
        public OptionalValue<Style> style { get; set; }
    }
    
    public class TodoEntry: UIComponent<TodoEntryProps>
    {
        private void HandleRemove()
        {
            Action e = Props.Get<TodoEntryProps>().onRemove;
            e?.Invoke();
        }
        
        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            string todo = Props.Get<TodoEntryProps>().todo;
            ResourceProvider<VisualElement> resProvider = GetProvider<ResourceProvider<VisualElement>>();
            
            ctx._<Box, StyleProps>(
                props: new StyleProps() { style = new Style() 
                {
                    height = new StyleLength(50),
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row),
                    justifyContent = new StyleEnum<Justify>(Justify.SpaceBetween),
                    alignItems = new StyleEnum<Align>(Align.Center), 
                }},
                render: ctx =>
                {
                    ctx._<Text, TextProps>(
                        props: new TextProps()
                        {
                            text = todo,
                            style = new Style()
                            {
                                fontSize = new StyleLength(20),
                                flexShrink = new StyleFloat(1),
                                flexGrow = new StyleFloat(1),
                                overflow = new StyleEnum<Overflow>(Overflow.Hidden),
                                textOverflow = new StyleEnum<TextOverflow>(TextOverflow.Ellipsis),
                            }
                        }
                    );
                    ctx._<Button, ButtonProps>(
                        props: new ButtonProps()
                        {
                            onClick = (Action)HandleRemove,
                            style = new Style()
                            {
                                width = new StyleLength(30),
                                height = new StyleLength(30),
                                backgroundImage = new StyleBackground(resProvider.GetResource<Sprite>("Kenny UI/Spritesheet/blueSheet/blue_button10.png")),
                            }
                        },
                        render: ctx => ctx._<Box, StyleProps>(
                            props: new StyleProps() { style = new Style() 
                            {
                                position = new StyleEnum<Position>(Position.Absolute),
                                width = new StyleLength(10),
                                height = new StyleLength(10),
                                top = new StyleLength(Length.Percent(50)),
                                left = new StyleLength(Length.Percent(50)),
                                translate = new StyleTranslate(new Translate(Length.Percent(-50), Length.Percent(-50), 0)),
                                backgroundImage = new StyleBackground(resProvider.GetResource<Sprite>("Kenny UI/Spritesheet/greySheet/grey_crossWhite.png")),
                            }}
                        )
                    );
                }
            );
        }
    }
}