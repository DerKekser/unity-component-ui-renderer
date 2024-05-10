using System;
using Examples.Todo.Providers;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine;
using UnityEngine.UIElements;
using Label = Kekser.ComponentSystem.ComponentUI.Components.Label;
using ScrollView = Kekser.ComponentSystem.ComponentUI.Components.ScrollView;

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

        public override void OnRender()
        {
            TodoProvider todoProvider = GetProvider<TodoProvider>();
            
            _<ScrollView, StyleProps>(
                props: new StyleProps() {
                    className = "w-[100%] h-[100%]",
                },
                render: () =>
                {
                    Each(todoProvider.GetTodos(), (todo, i) =>
                    {
                        _<TodoEntry, TodoEntryProps>(
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
                        _<Label, LabelProps>(
                            props: new LabelProps()
                            {
                                text = "No todos",
                                className = "color-grey font-20 mt-10 self-center",
                            }
                        );
                    }
                }
            );
        }
    }
}