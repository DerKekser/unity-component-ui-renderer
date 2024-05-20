using System.Collections.Generic;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.StateSystem;
using Kekser.ComponentSystem.ComponentUI;

namespace Examples.Todo.Providers
{
    public class TodoData
    {
        public string text;
        public bool done;
    }
    
    public class TodoProvider: UIContextProvider
    {
        private State<List<TodoData>> _todos;
        
        protected override void OnMount()
        {
            _todos = UseState(new List<TodoData>()
            {
                new TodoData() { text = "Buy milk", done = false },
                new TodoData() { text = "Feed the cat", done = false },
                new TodoData() { text = "Do the laundry", done = false }
            });
        }
        
        public void Add(string todo)
        {
            List<TodoData> todos = new List<TodoData>(_todos.Value);
            todos.Add(new TodoData() { text = todo, done = false });
            _todos.Value = todos;
        }
        
        public void Remove(TodoData todo)
        {
            List<TodoData> todos = new List<TodoData>(_todos.Value);
            todos.Remove(todo);
            _todos.Value = todos;
        }
        
        public void Toggle(TodoData todo)
        {
            List<TodoData> todos = new List<TodoData>(_todos.Value);
            int index = todos.FindIndex(t => t == todo);
            todos[index].done = !todos[index].done;
            _todos.Value = todos;
        }
        
        public TodoData Get(int index)
        {
            List<TodoData> todos = _todos.Value;
            return todos[index];
        }
        
        public void Clear()
        {
            _todos.Value = new List<TodoData>();
        }
        
        public List<TodoData> GetTodos()
        {
            return _todos.Value;
        }
        
        public int GetCount()
        {
            return _todos.Value.Count;
        }
    }
}