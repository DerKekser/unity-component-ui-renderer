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
            List<string> todos = OwnProps.todos.IsSet ? new List<string>((List<string>)OwnProps.todos) : new List<string>();
            todos.Add(todo);
            Props.Set(new TodoProviderProps() { todos = todos });
        }
        
        public void Remove(int index)
        {
            List<string> todos = OwnProps.todos.IsSet ? new List<string>((List<string>)OwnProps.todos) : new List<string>();
            todos.RemoveAt(index);
            Props.Set(new TodoProviderProps() { todos = todos });
        }
        
        public string Get(int index)
        {
            List<string> todos = OwnProps.todos.IsSet ? new List<string>((List<string>)OwnProps.todos) : new List<string>();
            return todos[index];
        }
        
        public void Clear()
        {
            Props.Set(new TodoProviderProps() { todos = new List<string>() });
        }
        
        public List<string> GetTodos()
        {
            return OwnProps.todos.IsSet ? new List<string>((List<string>)OwnProps.todos) : new List<string>();
        }
        
        public int GetCount()
        {
            return OwnProps.todos.IsSet ? ((List<string>)OwnProps.todos).Count : 0;
        }
    }
}