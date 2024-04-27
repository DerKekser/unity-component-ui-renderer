﻿using System;
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
        private NodeIndexHolder _nodeIndexHolder;
        private Props _props;
        
        private Transform _mainNode;
        private UIFragment _uiFragment;
        
        private Action<Context> _render;

        public Context Parent => _parent;
        public UIFragment UIFragment => _uiFragment;
        public Props Props => _props ??= new Props();

        public bool NeedsRerender => Props.IsDirty;
        
        // TODO: make abstract create separate context classes for different node types
        public Context(Transform mainNode)
        {
            _mainNode = mainNode;
            _contextHolder = new ContextHolder(this);
            _contextHolder.Reset();
            _usedContexts = new List<Context>();
            _nodeIndexHolder = new NodeIndexHolder();
            _props = new Props();
        }
        
        public Context(Context parent)
        {
            _mainNode = parent._mainNode;
            _parent = parent;
            _contextHolder = new ContextHolder(this);
            _contextHolder.Reset();
            _usedContexts = new List<Context>();
            _nodeIndexHolder = new NodeIndexHolder();
            _props = new Props();
        }
        
        public void SetRender(Action<Context> render)
        {
            _render = render;
        }
        
        public void Traverse()
        {
            _contextHolder.Reset();
            if (NeedsRerender)
            {
                Props.IsDirty = false;
                _usedContexts.Clear();
                if (_uiFragment != null)
                    _uiFragment.Render(_render);
                else
                    _render?.Invoke(this);

                foreach (Context context in _contextHolder.GetContexts().Except(_usedContexts).ToArray())
                {
                    context.Remove();
                    _contextHolder.Remove(context);
                }
            }
            
            foreach (Context child in _contextHolder.GetContexts())
            {
                child.Traverse();
                _nodeIndexHolder.UpdateNode(child);
            }
        }
        
        private void Remove()
        {
            foreach (Context child in _contextHolder.GetContexts())
            {
                child.Remove();
            }
            _uiFragment.Unmount();
        }

        private Context Child<TComponent>(int? key) where TComponent : UIFragment
        {
            Context context = _contextHolder.Get(key);
            _usedContexts.Add(context);

            if (context._uiFragment != null)
            {
                context._contextHolder.Reset();
                _nodeIndexHolder.SetIndex(context, _contextHolder.Index);
                return context;
            }
            
            context._uiFragment = Activator.CreateInstance<TComponent>();
            context._uiFragment.SetContext(context);
            context._uiFragment.Mount(_uiFragment?.Node ? _uiFragment?.Node : _mainNode);
            _nodeIndexHolder.SetIndex(context, _contextHolder.Index);
            return context;
        }
        
        public void _<TComponent>(string key = null, Action<Props> props = null, Action<Context> render = null) where TComponent : UIFragment
        {
            // TODO: improve key generation algorithm to identify components
            int? hash = key?.GetHashCode() ?? typeof(TComponent).GetHashCode();

            Context child = Child<TComponent>(hash);
            _nodeIndexHolder.UpdateNode(child);
            child.SetRender(render);
            if (props != null)
                props.Invoke(child.Props);
            
            child.Props.IsDirty = true;
            child.Traverse();
        }
    }
}