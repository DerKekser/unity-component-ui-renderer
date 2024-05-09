using System;
using Examples.Todo.Providers;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.UIElements;
using Button = Kekser.ComponentSystem.ComponentUI.Components.Button;
using GroupBox = Kekser.ComponentSystem.ComponentUI.Components.GroupBox;
using Label = Kekser.ComponentSystem.ComponentUI.Components.Label;

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
        
        public override void OnRender()
        {
            TodoData todo = OwnProps.todo;
            
            _<Group, StyleProps>(
                props: new StyleProps() { style = new Style() 
                {
                    height = new StyleLength(50),
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row),
                    justifyContent = new StyleEnum<Justify>(Justify.SpaceBetween),
                    alignItems = new StyleEnum<Align>(Align.Center), 
                }},
                render: () =>
                {
                    _<Button, ButtonProps>(
                        props: new ButtonProps()
                        {
                            onClick = (Action)HandleToggle,
                            className = "todo-done",
                            style = new Style()
                            {
                                width = new StyleLength(30),
                                height = new StyleLength(30),
                                marginRight = new StyleLength(5),
                            }
                        },
                        render: () =>
                        {
                            if (!todo.done) return;
                            _<Group, StyleProps>(
                                props: new StyleProps() { 
                                    className = "icon",
                                    style = new Style() 
                                    {
                                        position = new StyleEnum<Position>(Position.Absolute),
                                        width = new StyleLength(10),
                                        height = new StyleLength(10),
                                        top = new StyleLength(Length.Percent(50)),
                                        left = new StyleLength(Length.Percent(50)),
                                        translate = new StyleTranslate(new Translate(Length.Percent(-50), Length.Percent(-50), 0)),
                                    }
                                }
                            );
                        }
                    );
                    _<Label, LabelProps>(
                        props: new LabelProps()
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
                    _<Button, ButtonProps>(
                        props: new ButtonProps()
                        {
                            onClick = (Action)HandleRemove,
                            className = "todo-remove",
                            style = new Style()
                            {
                                width = new StyleLength(30),
                                height = new StyleLength(30),
                                marginLeft = new StyleLength(5),
                            }
                        },
                        render: () => _<GroupBox, GroupBoxProps>(
                            props: new GroupBoxProps() { 
                                className = "icon",
                                style = new Style() 
                                {
                                    position = new StyleEnum<Position>(Position.Absolute),
                                    width = new StyleLength(10),
                                    height = new StyleLength(10),
                                    top = new StyleLength(Length.Percent(50)),
                                    left = new StyleLength(Length.Percent(50)),
                                    translate = new StyleTranslate(new Translate(Length.Percent(-50), Length.Percent(-50), 0)),
                                }
                            }
                        )
                    );
                }
            );
        }
    }
}