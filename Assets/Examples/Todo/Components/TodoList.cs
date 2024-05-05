﻿using System;
using Examples.Todo.Providers;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using UnityEngine;
using UnityEngine.UIElements;

namespace Examples.Todo.Components
{
    public class TodoList: UIComponent
    {
        private void HandleToggle(TodoData todo)
        {
            TodoProvider provider = GetProvider<TodoProvider>();
            provider.Toggle(todo);
        }
        
        private void HandleRemove(TodoData todo)
        {
            TodoProvider provider = GetProvider<TodoProvider>();
            provider.Remove(todo);
        }

        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            TodoProvider todoProvider = GetProvider<TodoProvider>();
            
            ctx._<ScrollArea, StyleProps>(
                props: new StyleProps() { style = new Style() 
                {
                    height = new StyleLength(Length.Percent(100)),
                    width = new StyleLength(Length.Percent(100)),
                }},
                render: ctx =>
                {
                    ctx.Each(todoProvider.GetTodos(), (todo, i) =>
                    {
                        ctx._<TodoEntry, TodoEntryProps>(
                            key: i.ToString(),
                            props: new TodoEntryProps()
                            {
                                todo = todo,
                                onToggle = new Action(() => HandleToggle(todo)),
                                onRemove = new Action(() => HandleRemove(todo)),
                            }
                        );
                    });
                    if (todoProvider.GetTodos().Count == 0)
                    {
                        ctx._<Text, TextProps>(
                            props: new TextProps()
                            {
                                text = "No todos",
                                style = new Style()
                                {
                                    color = new StyleColor(Color.grey),
                                    fontSize = new StyleLength(20),
                                    alignSelf = new StyleEnum<Align>(Align.Center),
                                    marginTop = new StyleLength(10),
                                }
                            }
                        );
                    }
                }
            );
        }
    }
}