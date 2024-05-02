using System.Collections.Generic;
using UnityEngine;

namespace Kekser.ComponentSystem.ComponentBase
{
    public abstract class BaseContextHolder<TNode> where TNode: class, new()
    {
        private Dictionary<int, List<BaseContext<TNode>>> _contexts;
        private Dictionary<int, int> _subIndexes;
        
        private BaseContext<TNode> _context;
        
        public BaseContextHolder(BaseContext<TNode> context)
        {
            _context = context;
            _contexts = new Dictionary<int, List<BaseContext<TNode>>>();
            _subIndexes = new Dictionary<int, int>();
            
            Reset();
        }

        public void Reset()
        {
            _subIndexes.Clear();
        }
        
        public BaseContext<TNode> Get(int? key)
        {
            int index = key ?? 0;
            int subIndex = _subIndexes.TryGetValue(index, out int i) ? i : 0;
            
            if (!_contexts.TryGetValue(index, out List<BaseContext<TNode>> contexts))
            {
                contexts = new List<BaseContext<TNode>>();
                _contexts.Add(index, contexts);
            }
            
            if (contexts.Count <= subIndex)
            {
                BaseContext<TNode> child = CreateContext(_context);
                contexts.Add(child);
            }

            if (subIndex > 0)
            {
                Debug.LogWarning("Multiple contexts with the same key are not supported! It might lead to unexpected behavior.");
            }
            
            BaseContext<TNode> context = contexts[subIndex];
            
            _subIndexes[index] = subIndex + 1;
            
            return context;
        }
        
        protected abstract BaseContext<TNode> CreateContext(BaseContext<TNode> parent);
        
        public void Remove(BaseContext<TNode> context)
        {
            foreach (List<BaseContext<TNode>> contexts in _contexts.Values)
            {
                if (contexts.Remove(context))
                    return;
            }
        }
        
        public IEnumerable<BaseContext<TNode>> GetContexts()
        {
            foreach (List<BaseContext<TNode>> contexts in _contexts.Values)
            {
                foreach (BaseContext<TNode> context in contexts)
                {
                    yield return context;
                }
            }
        }
    }
}