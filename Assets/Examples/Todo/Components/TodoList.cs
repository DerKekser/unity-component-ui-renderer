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
        TodoProvider _todoProvider;
        
        protected override void OnMount()
        {
            _todoProvider = UseContextProvider<TodoProvider>();
        }

        private void HandleToggle(TodoData todo)
        {
            _todoProvider.Toggle(todo);
        }
        
        private void HandleRemove(TodoData todo)
        {
            _todoProvider.Remove(todo);
        }

        protected override void OnRender()
        {
            _<ScrollView, StyleProps>(
                props: new StyleProps() {
                    className = "w-[100%] h-[100%]",
                },
                render: () =>
                {
                    Each(_todoProvider.GetTodos(), (todo, i) =>
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
                    if (_todoProvider.GetTodos().Count == 0)
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