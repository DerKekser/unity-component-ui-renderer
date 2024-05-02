using System.Collections.Generic;
using Kekser.ComponentSystem.ComponentUI;

namespace Examples.Todo.Providers
{
    public class TodoProvider: UIProvider
    {
        public override void OnMount()
        {
            Props.Set("todos", new List<string>()
            {
                "Buy milk",
                "Feed the cat",
                "Do the laundry"
            });
        }

        public void Add(string todo)
        {
            List<string> todos = new List<string>(Props.Get("todos", new List<string>()));
            todos.Add(todo);
            Props.Set("todos", todos);
        }
        
        public void Remove(int index)
        {
            List<string> todos = new List<string>(Props.Get("todos", new List<string>()));
            todos.RemoveAt(index);
            Props.Set("todos", todos);
        }
        
        public string Get(int index)
        {
            return Props.Get("todos", new List<string>())[index];
        }
        
        public void Clear()
        {
            Props.Set("todos", new List<string>());
        }
        
        public List<string> GetTodos()
        {
            return Props.Get("todos", new List<string>());
        }
        
        public int GetCount()
        {
            return Props.Get("todos", new List<string>()).Count;
        }
    }
}