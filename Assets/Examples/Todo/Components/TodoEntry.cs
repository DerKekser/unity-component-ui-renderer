﻿using System;
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
                props: new StyleProps() { 
                    className = "h-50 flex-row justify-between items-center",
                },
                render: () =>
                {
                    _<Button, ButtonProps>(
                        props: new ButtonProps()
                        {
                            onClick = (Action)HandleToggle,
                            className = "todo-done w-30 h-30 mr-5",
                        },
                        render: () =>
                        {
                            if (!todo.done) return;
                            _<Group, StyleProps>(
                                props: new StyleProps() { 
                                    className = "icon absolute w-10 h-10 t-[50%] l-[50%] translate-[-50%]",
                                }
                            );
                        }
                    );
                    _<Label, LabelProps>(
                        props: new LabelProps()
                        {
                            text = todo.text,
                            className = "font-20 flex-1 overflow-hidden text-ellipsis",
                        }
                    );
                    _<Button, ButtonProps>(
                        props: new ButtonProps()
                        {
                            onClick = (Action)HandleRemove,
                            className = "todo-remove w-30 h-30 ml-5",
                        },
                        render: () => _<GroupBox, GroupBoxProps>(
                            props: new GroupBoxProps() { 
                                className = "icon absolute w-10 h-10 t-[50%] l-[50%] translate-[-50%]",
                            }
                        )
                    );
                }
            );
        }
    }
}