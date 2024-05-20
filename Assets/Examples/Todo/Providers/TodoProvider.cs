using System.Collections.Generic;
using Kekser.ComponentSystem.ComponentBase;
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
    
    public class TodoProvider: UIContextProvider<TodoProviderProps>
    {
        public override ContextProviderProps<TodoProviderProps> DefaultProps { get; } = new ContextProviderProps<TodoProviderProps>()
        {
            value = new TodoProviderProps()
            {
                todos = new List<TodoData>()
                {
                    new TodoData() { text = "Buy milk", done = false },
                    new TodoData() { text = "Feed the cat", done = false },
                    new TodoData() { text = "Do the laundry", done = false }
                }
            }
        };

        public void Add(string todo)
        {
            List<TodoData> todos = ProviderProps.todos.IsSet ? new List<TodoData>((List<TodoData>)ProviderProps.todos) : new List<TodoData>();
            todos.Add(new TodoData() { text = todo, done = false });
            ProviderProps = new TodoProviderProps() { todos = todos };
        }
        
        public void Remove(TodoData todo)
        {
            List<TodoData> todos = ProviderProps.todos.IsSet ? new List<TodoData>((List<TodoData>)ProviderProps.todos) : new List<TodoData>();
            todos.Remove(todo);
            ProviderProps = new TodoProviderProps() { todos = todos };
        }
        
        public void Toggle(TodoData todo)
        {
            List<TodoData> todos = ProviderProps.todos.IsSet ? new List<TodoData>((List<TodoData>)ProviderProps.todos) : new List<TodoData>();
            int index = todos.FindIndex(t => t == todo);
            todos[index].done = !todos[index].done;
            ProviderProps = new TodoProviderProps() { todos = todos };
        }
        
        public TodoData Get(int index)
        {
            List<TodoData> todos = ProviderProps.todos.IsSet ? new List<TodoData>((List<TodoData>)ProviderProps.todos) : new List<TodoData>();
            return todos[index];
        }
        
        public void Clear()
        {
            ProviderProps = new TodoProviderProps() { todos = new List<TodoData>() };
        }
        
        public List<TodoData> GetTodos()
        {
            return ProviderProps.todos.IsSet ? new List<TodoData>((List<TodoData>)ProviderProps.todos) : new List<TodoData>();
        }
        
        public int GetCount()
        {
            return ProviderProps.todos.IsSet ? ((List<TodoData>)ProviderProps.todos).Count : 0;
        }
    }
}