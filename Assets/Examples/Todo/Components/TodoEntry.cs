using System;
using Examples.Todo.Providers;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.Extension.ResourceManagement;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine;
using UnityEngine.UIElements;
using Box = Kekser.ComponentSystem.ComponentUI.Components.Box;
using Button = Kekser.ComponentSystem.ComponentUI.Components.Button;

namespace Examples.Todo.Components
{
    public class TodoEntryProps: StyleProps
    {
        public ObligatoryValue<TodoData> todo { get; set; } = new();
        public ObligatoryValue<Action> onToggle { get; set; } = new();
        public ObligatoryValue<Action> onRemove { get; set; } = new();
    }
    
    public class TodoEntry: UIComponent<TodoEntryProps>
    {
        private void HandleToggle()
        {
            Action e = OwnProps.onToggle;
            e?.Invoke();
        }
        
        private void HandleRemove()
        {
            Action e = OwnProps.onRemove;
            e?.Invoke();
        }
        
        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            TodoData todo = OwnProps.todo;
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
                    ctx._<Button, ButtonProps>(
                        props: new ButtonProps()
                        {
                            onClick = (Action)HandleToggle,
                            style = new Style()
                            {
                                width = new StyleLength(30),
                                height = new StyleLength(30),
                                marginRight = new StyleLength(5),
                                backgroundImage = new StyleBackground(resProvider.GetResource<Sprite>("3d456c5ff9b4bb14981f2428d2c17e31--3244089793890354390@grey_button13.png")),
                            }
                        },
                        render: ctx =>
                        {
                            if (!todo.done) return;
                            ctx._<Box, StyleProps>(
                                props: new StyleProps() { style = new Style() 
                                {
                                    position = new StyleEnum<Position>(Position.Absolute),
                                    width = new StyleLength(10),
                                    height = new StyleLength(10),
                                    top = new StyleLength(Length.Percent(50)),
                                    left = new StyleLength(Length.Percent(50)),
                                    translate = new StyleTranslate(new Translate(Length.Percent(-50), Length.Percent(-50), 0)),
                                    backgroundImage = new StyleBackground(resProvider.GetResource<Sprite>("d1023af4809dfc74ea55d04ae9bfe123--7938629432754144731@blue_checkmark.png")),
                                }}
                            );
                        }
                    );
                    ctx._<Text, TextProps>(
                        props: new TextProps()
                        {
                            text = todo.text,
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
                                marginLeft = new StyleLength(5),
                                backgroundImage = new StyleBackground(resProvider.GetResource<Sprite>("d1023af4809dfc74ea55d04ae9bfe123-7285192305131594788@blue_button10.png")),
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
                                backgroundImage = new StyleBackground(resProvider.GetResource<Sprite>("3d456c5ff9b4bb14981f2428d2c17e31-8517964494106879923@grey_crossWhite.png")),
                            }}
                        )
                    );
                }
            );
        }
    }
}