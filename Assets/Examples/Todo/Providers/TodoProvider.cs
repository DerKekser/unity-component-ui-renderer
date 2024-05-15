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
            List<TodoData> todos = Props.todos.IsSet ? new List<TodoData>((List<TodoData>)Props.todos) : new List<TodoData>();
            todos.Add(new TodoData() { text = todo, done = false });
            Props = new TodoProviderProps() { todos = todos };
        }
        
        public void Remove(TodoData todo)
        {
            List<TodoData> todos = Props.todos.IsSet ? new List<TodoData>((List<TodoData>)Props.todos) : new List<TodoData>();
            todos.Remove(todo);
            Props = new TodoProviderProps() { todos = todos };
        }
        
        public void Toggle(TodoData todo)
        {
            List<TodoData> todos = Props.todos.IsSet ? new List<TodoData>((List<TodoData>)Props.todos) : new List<TodoData>();
            int index = todos.FindIndex(t => t == todo);
            todos[index].done = !todos[index].done;
            Props = new TodoProviderProps() { todos = todos };
        }
        
        public TodoData Get(int index)
        {
            List<TodoData> todos = Props.todos.IsSet ? new List<TodoData>((List<TodoData>)Props.todos) : new List<TodoData>();
            return todos[index];
        }
        
        public void Clear()
        {
            Props = new TodoProviderProps() { todos = new List<TodoData>() };
        }
        
        public List<TodoData> GetTodos()
        {
            return Props.todos.IsSet ? new List<TodoData>((List<TodoData>)Props.todos) : new List<TodoData>();
        }
        
        public int GetCount()
        {
            return Props.todos.IsSet ? ((List<TodoData>)Props.todos).Count : 0;
        }
    }
}