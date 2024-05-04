using System.Collections.Generic;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.PropSystem.Rework;
using Kekser.ComponentSystem.ComponentUI;

namespace Examples.Todo.Providers
{
    public struct TodoProviderProps
    {
        public OptionalValue<List<string>> todos { get; set; }
    }
    
    public class TodoProvider: UIProvider<TodoProviderProps>
    {
        public override void OnMount()
        {
            Props.Set(new TodoProviderProps() { todos = new List<string>() { "Buy milk", "Feed the cat", "Do the laundry" } });
        }

        /*public override IProp[] DefaultProps => new IProp[]
        {
            new Prop("todos", new List<string>()
            {
                "Buy milk",
                "Feed the cat",
                "Do the laundry"
            })
        };*/

        public void Add(string todo)
        {
            Props.Set<TodoProviderProps>(props =>
            {
                List<string> todos = props.todos.IsSet ? new List<string>(props.todos.Value) : new List<string>();
                todos.Add(todo);
                props.todos = todos;
                return props;
            });
        }
        
        public void Remove(int index)
        {
            Props.Set<TodoProviderProps>(props =>
            {
                List<string> todos = props.todos.IsSet ? new List<string>(props.todos.Value) : new List<string>();
                todos.RemoveAt(index);
                props.todos = todos;
                return props;
            });
        }
        
        public string Get(int index)
        {
            return Props.Get<TodoProviderProps>().todos.Value[index];
        }
        
        public void Clear()
        {
            Props.Set<TodoProviderProps>(props =>
            {
                props.todos = new List<string>();
                return props;
            });
        }
        
        public List<string> GetTodos()
        {
            return Props.Get<TodoProviderProps>().todos.Value;
        }
        
        public int GetCount()
        {
            return Props.Get<TodoProviderProps>().todos.Value.Count;
        }
    }
}