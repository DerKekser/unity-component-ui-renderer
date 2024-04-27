using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kekser.ComponentUI
{
    public class Context
    {
        private Context _parent;
        private ContextHolder _contextHolder;
        private List<Context> _usedContexts;
        private Props _props;
        
        private Transform _mainNode;
        private UIComponent _uiComponent;

        public Props Props => _props ??= new Props();

        public bool NeedsRerender => Props.IsDirty;
        
        public Context(Transform mainNode)
        {
            _mainNode = mainNode;
            _contextHolder = new ContextHolder(this);
            _contextHolder.Reset();
            _usedContexts = new List<Context>();
            _props = new Props();
        }
        
        public Context(Context parent)
        {
            _mainNode = parent._mainNode;
            _parent = parent;
            _contextHolder = new ContextHolder(this);
            _contextHolder.Reset();
            _usedContexts = new List<Context>();
            _props = new Props();
        }
        
        public void Traverse(Action<Context> render)
        {
            _contextHolder.Reset();
            if (NeedsRerender)
            {
                Props.IsDirty = false;
                _usedContexts.Clear();
                if (_uiComponent != null)
                    _uiComponent.Render(render);
                else
                    render?.Invoke(this);

                foreach (Context context in _contextHolder.GetContexts().Except(_usedContexts).ToArray())
                {
                    context.Remove();
                    _contextHolder.Remove(context);
                }
                
                return;
            }
            
            foreach (Context child in _contextHolder.GetContexts())
            {
                child.Traverse(render);
            }
        }
        
        private void Remove()
        {
            foreach (Context child in _contextHolder.GetContexts())
            {
                child.Remove();
            }
            _uiComponent.Unmount();
        }

        private Context Child<TComponent>(int? key) where TComponent : UIComponent
        {
            Context context = _contextHolder.Get(key);
            _usedContexts.Add(context);

            if (context._uiComponent != null)
            {
                context._contextHolder.Reset();
                context._uiComponent.Node.SetSiblingIndex(_contextHolder.Index);
                return context;
            }
            
            context._uiComponent = Activator.CreateInstance<TComponent>();
            context._uiComponent.SetContext(context);
            context._uiComponent.Mount(_uiComponent?.Node ?? _mainNode);
            context._uiComponent.Node.SetSiblingIndex(_contextHolder.Index);
            return context;
        }
        
        public void _<TComponent>(string key = null, Action<Props> props = null, Action<Context> render = null) where TComponent : UIComponent
        {
            int? hash = key?.GetHashCode() ?? render?.GetHashCode() ?? typeof(TComponent).GetHashCode();

            Context child = Child<TComponent>(hash);
            if (props != null)
                props.Invoke(child.Props);
            
            child.Props.IsDirty = true;
            child.Traverse(render);
        }
    }
}