using System.Collections.Generic;
using UnityEngine;

namespace Kekser.ComponentUI
{
    public class ContextHolder
    {
        private Dictionary<int, List<Context>> _contexts;
        private int _index;
        private Dictionary<int, int> _subIndexes;
        
        private Context _context;
        
        public int Index => _index;
        
        public ContextHolder(Context context)
        {
            _context = context;
            _contexts = new Dictionary<int, List<Context>>();
            _subIndexes = new Dictionary<int, int>();
            
            Reset();
        }

        public void Reset()
        {
            _index = 0;
            _subIndexes.Clear();
        }
        
        public Context Get(int? key)
        {
            int index = key ?? 0;
            int subIndex = _subIndexes.TryGetValue(index, out int i) ? i : 0;
            
            if (!_contexts.TryGetValue(index, out List<Context> contexts))
            {
                contexts = new List<Context>();
                _contexts.Add(index, contexts);
            }
            
            if (contexts.Count <= subIndex)
            {
                Context child = new Context(_context);
                contexts.Add(child);
            }

            if (subIndex > 0)
            {
                Debug.LogWarning("Multiple contexts with the same key are not supported! It might lead to unexpected behavior.");
            }
            
            Context context = contexts[subIndex];
            
            _index++;
            _subIndexes[index] = subIndex + 1;
            
            return context;
        }
        
        public void Remove(Context context)
        {
            foreach (List<Context> contexts in _contexts.Values)
            {
                if (contexts.Remove(context))
                    return;
            }
        }
        
        public IEnumerable<Context> GetContexts()
        {
            foreach (List<Context> contexts in _contexts.Values)
            {
                foreach (Context context in contexts)
                {
                    yield return context;
                }
            }
        }
    }
}