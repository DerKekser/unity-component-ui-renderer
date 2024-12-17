using System.Collections.Generic;

namespace Kekser.ComponentSystem.V2.ComponentBase
{
    public class FragmentContext : IFragmentContext
    {
        private IFragment _fragment;
        private int? _key;
        private object _props;
        private List<IFragmentContext> _children = new List<IFragmentContext>();
        private IFragmentContext _internalChildren;
        
        public int? Key => _key;
        
        public FragmentContext(IFragment fragment)
        {
            _fragment = fragment;
            _internalChildren = _fragment?.GetContext();
        }
        
        public void SetKey(int? key = null)
        {
            if (key.HasValue)
                _key = key;
        }
        
        public void SetProps(object props = null)
        {
            _props = props;
        }
        
        public void SetChildren(params IFragmentContext[] children)
        {
            _children = new List<IFragmentContext>();
            AddChildren(children);
        }
        
        public void AddChildren(params IFragmentContext[] children)
        {
            for (int i = 0; i < children.Length; i++)
            {
                IFragmentContext child = children[i];
                if (child == null)
                    continue;
                
                if (!child.Key.HasValue)
                    child.SetKey(i);
            }
            _children.AddRange(children);
        }
        
        public IFragmentContext _(params IFragmentContext[] children)
        {
            AddChildren(children);
            return this;
        }
    }
    
    public class ChildrenTargetFragmentContext : FragmentContext
    {
        public ChildrenTargetFragmentContext(IFragment fragment) : base(fragment)
        {
            
        }
    }
}