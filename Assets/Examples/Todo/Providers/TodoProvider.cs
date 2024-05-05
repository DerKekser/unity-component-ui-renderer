using System.Collections.Generic;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;

namespace Examples.Todo.Providers
{
    public class TodoData
    {
        public string text;
        public bool done;
    }
    
    public class TodoProviderProps
    {
        public OptionalValue<List<TodoData>> todos { get; set; } = new();
    }
    
    public class TodoProvider: UIProvider<TodoProviderProps>
    {
        public override TodoProviderProps DefaultProps { get; } = new TodoProviderProps()
        {
            todos = new List<TodoData>()
            {
                new TodoData() { text = "Buy milk", done = false },
                new TodoData() { text = "Feed the cat", done = false },
                new TodoData() { text = "Do the laundry", done = false }
            }
        };

        public void Add(string todo)
        {
            List<TodoData> todos = OwnProps.todos.IsSet ? new List<TodoData>((List<TodoData>)OwnProps.todos) : new List<TodoData>();
            todos.Add(new TodoData() { text = todo, done = false });
            Props.Set(new TodoProviderProps() { todos = todos });
        }
        
        public void Remove(TodoData todo)
        {
            List<TodoData> todos = OwnProps.todos.IsSet ? new List<TodoData>((List<TodoData>)OwnProps.todos) : new List<TodoData>();
            todos.Remove(todo);
            Props.Set(new TodoProviderProps() { todos = todos });
        }
        
        public void Toggle(TodoData todo)
        {
            List<TodoData> todos = OwnProps.todos.IsSet ? new List<TodoData>((List<TodoData>)OwnProps.todos) : new List<TodoData>();
            int index = todos.FindIndex(t => t == todo);
            todos[index].done = !todos[index].done;
            Props.Set(new TodoProviderProps() { todos = todos });
        }
        
        public TodoData Get(int index)
        {
            List<TodoData> todos = OwnProps.todos.IsSet ? new List<TodoData>((List<TodoData>)OwnProps.todos) : new List<TodoData>();
            return todos[index];
        }
        
        public void Clear()
        {
            Props.Set(new TodoProviderProps() { todos = new List<TodoData>() });
        }
        
        public List<TodoData> GetTodos()
        {
            return OwnProps.todos.IsSet ? new List<TodoData>((List<TodoData>)OwnProps.todos) : new List<TodoData>();
        }
        
        public int GetCount()
        {
            return OwnProps.todos.IsSet ? ((List<TodoData>)OwnProps.todos).Count : 0;
        }
    }
}