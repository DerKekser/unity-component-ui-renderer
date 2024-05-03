using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Kekser.ComponentSystem.ComponentBase.PropSystem;

namespace Kekser.ComponentSystem.ComponentBase
{
    public abstract class BaseContext<TNode> where TNode: class, new()
    {
        private BaseContext<TNode> _parent;
        private BaseContextHolder<TNode> _contextHolder;
        private List<BaseContext<TNode>> _usedContexts;
        private Props _props;
        
        private TNode _mainNode;
        private BaseFragment<TNode> _fragment;
        
        private Action<BaseContext<TNode>> _render;

        public BaseContext<TNode> Parent => _parent;
        public BaseFragment<TNode> Fragment => _fragment;
        public Props Props => _props ??= new Props();

        public bool NeedsRerender => Props.IsDirty;
        
        // TODO: make abstract create separate context classes for different node types
        public BaseContext(TNode mainNode)
        {
            _mainNode = mainNode;
            _contextHolder = CreateContextHolder(this);
            _contextHolder.Reset();
            _usedContexts = new List<BaseContext<TNode>>();
            _props = new Props();
        }
        
        public BaseContext(BaseContext<TNode> parent)
        {
            _mainNode = parent._mainNode;
            _parent = parent;
            _contextHolder = CreateContextHolder(this);
            _contextHolder.Reset();
            _usedContexts = new List<BaseContext<TNode>>();
            _props = new Props();
        }
        
        protected abstract BaseContextHolder<TNode> CreateContextHolder(BaseContext<TNode> context);
        
        public void SetRender(Action<BaseContext<TNode>> render)
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
                if (_fragment != null)
                    _fragment.Render();
                else
                    Render(this);

                foreach (BaseContext<TNode> context in _contextHolder.GetContexts().Except(_usedContexts).ToArray())
                {
                    context.Remove();
                    _contextHolder.Remove(context);
                }
                
                return;
            }
            
            foreach (BaseContext<TNode> child in _contextHolder.GetContexts())
            {
                child.Traverse();
            }
        }

        public void Render(BaseContext<TNode> ctx)
        {
            _render?.Invoke(ctx);
        }
        
        private void Remove()
        {
            foreach (BaseContext<TNode> child in _contextHolder.GetContexts())
            {
                child.Remove();
            }
            _fragment.Unmount();
        }

        private BaseContext<TNode> Child<TComponent>(int? key) where TComponent : BaseFragment<TNode>
        {
            BaseContext<TNode> context = _contextHolder.Get(key);
            _usedContexts.Add(context);

            if (context._fragment != null)
            {
                context._contextHolder.Reset();
                return context;
            }
            
            context._fragment = Activator.CreateInstance<TComponent>();
            context._fragment.SetContext(context);
            context._fragment.Mount(_fragment?.FragmentNode ?? _mainNode);
            return context;
        }
        
        public void Each<T>(IEnumerable<T> props, Action<T, int> callback)
        {
            int i = 0;
            foreach (T prop in props)
            {
                callback?.Invoke(prop, i++);
            }
        }
        
        public TComponent _<TComponent>(string key = null, Action<BaseContext<TNode>> render = null, [CallerLineNumber] int callerLine = 0, params IProp[] props) where TComponent : BaseFragment<TNode>
        {
            int? hash = key?.GetHashCode() ?? callerLine.GetHashCode();

            BaseContext<TNode> child = Child<TComponent>(hash);
            SetNodeAsLastSibling(child._fragment.FragmentRoot);
            child.SetRender(render);
            foreach (IProp prop in props)
            {
                prop.AddToProps(child.Props);
            }
            
            child.Props.IsDirty = true;
            child.Traverse();
            
            return (TComponent) child._fragment;
        }
        
        protected abstract void SetNodeAsLastSibling(TNode node);
    }
}