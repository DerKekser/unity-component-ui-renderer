using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scenes.Kekser.ComponentUI
{
    public class Context
    {
        private Context _parent;
        private Dictionary<int, Context> _children;
        private List<int> _usedChildren;
        private Props _props;
        
        private UIComponent _uiComponent;

        private int _index;
        
        public Props Props
        {
            get
            {
                if (_props == null)
                    _props = new Props();
                return _props;
            }
        }
        
        public bool NeedsRerender => Props.IsDirty;
        
        public Context(Context parent)
        {
            _parent = parent;
            _children = new Dictionary<int, Context>();
            _usedChildren = new List<int>();
            _index = 0;
        }
        
        public void Traverse(Action<Context> render)
        {
            _index = 0;
            if (NeedsRerender)
            {
                Props.IsDirty = false;
                _usedChildren.Clear();
                if (_uiComponent != null)
                    _uiComponent.Render(render);
                else
                    render?.Invoke(this);

                foreach (int key in _children.Keys.Except(_usedChildren).ToArray())
                {
                    _children[key]._uiComponent.Unmount();
                    _children.Remove(key);
                }
                
                return;
            }
            
            foreach (Context child in _children.Values)
            {
                child.Traverse(render);
            }
        }

        private Context Child<TComponent>(int key) where TComponent : UIComponent
        {
            _usedChildren.Add(key);
            if (!_children.TryGetValue(key, out Context child))
            {
                child = new Context(this);
                _children.Add(key.GetHashCode(), child);
                child._uiComponent = Activator.CreateInstance<TComponent>();
                child._uiComponent.SetContext(child);
                child._props = new Props();
                child._uiComponent.Mount(_uiComponent?.Node);
            }
            child._index = 0;
            child._uiComponent.Node.SetSiblingIndex(_index);
            return child;
        }
        
        public void _<TComponent>(string key = null, Action<Props> props = null, Action<Context> render = null) where TComponent : UIComponent
        {
            _index++;
            int hash = key?.GetHashCode() ?? render?.GetHashCode() ?? _index;

            Context child = Child<TComponent>(hash);
            if (props != null)
                props.Invoke(child.Props);
            
            child.Props.IsDirty = true;
            child.Traverse(render);
        }
    }
}