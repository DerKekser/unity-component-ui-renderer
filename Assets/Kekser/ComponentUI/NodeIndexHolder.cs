using System.Collections.Generic;

namespace Kekser.ComponentUI
{
    public class NodeIndexHolder
    {
        private Dictionary<Context, int> _contextSiblingIndexes;
        
        public NodeIndexHolder()
        {
            _contextSiblingIndexes = new Dictionary<Context, int>();
        }
        
        public void SetIndex(Context context, int index)
        {
            _contextSiblingIndexes[context] = index;
        }
        
        public void UpdateNode(Context context)
        {
            if (!_contextSiblingIndexes.TryGetValue(context, out int index) || context.UIFragment.Node.GetSiblingIndex() == index)
                return;
            
            context.UIFragment.Node.SetSiblingIndex(index);
        }
    }
}