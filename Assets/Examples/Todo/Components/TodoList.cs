using System;
using Examples.Todo.Providers;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.Extension.ResourceManagement;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using UnityEngine.UIElements;

namespace Examples.Todo.Components
{
    public class TodoList: UIComponent
    {
        private void HandleRemove(int index)
        {
            TodoProvider provider = GetProvider<TodoProvider>();
            provider.Remove(index);
        }

        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            TodoProvider todoProvider = GetProvider<TodoProvider>();
            ResourceProvider<VisualElement> resProvider = GetProvider<ResourceProvider<VisualElement>>();
            
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
                                onRemove = new Action(() => HandleRemove(i)),
                            }
                        );
                    });
                }
            );
        }
    }
}