using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.PropSystem.Rework;

namespace Kekser.ComponentSystem.ComponentBase
{
    public abstract class BaseContext<TNode> where TNode: class, new()
    {
        private BaseContext<TNode> _parent;
        private BaseContextHolder<TNode> _contextHolder;
        private List<BaseContext<TNode>> _usedContexts;
        private IPropList _propList;
        
        private TNode _mainNode;
        private IFragment<TNode> _fragment;
        
        private Action<BaseContext<TNode>> _render;

        public BaseContext<TNode> Parent => _parent;
        public IFragment<TNode> Fragment => _fragment;
        public IPropList PropList => _propList;

        public bool NeedsRerender => PropList.IsDirty;
        
        // TODO: make abstract create separate context classes for different node types
        public BaseContext(TNode mainNode)
        {
            _mainNode = mainNode;
            _contextHolder = CreateContextHolder(this);
            _contextHolder.Reset();
            _usedContexts = new List<BaseContext<TNode>>();
        }
        
        public BaseContext(BaseContext<TNode> parent)
        {
            _mainNode = parent._mainNode;
            _parent = parent;
            _contextHolder = CreateContextHolder(this);
            _contextHolder.Reset();
            _usedContexts = new List<BaseContext<TNode>>();
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
                PropList.IsDirty = false;
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
        
        private BaseContext<TNode> Child<TComponent, TProps>(int? key) where TComponent : BaseFragment<TNode> where TProps : struct
        {
            BaseContext<TNode> context = _contextHolder.Get(key);
            _usedContexts.Add(context);

            if (context._fragment != null)
            {
                context._contextHolder.Reset();
                return context;
            }

            context._propList = new PropList<TProps>();
            
            context._fragment = Activator.CreateInstance<TComponent>();
            context._fragment.SetContext(context);
            
            /*foreach (IProp prop in context._fragment.DefaultProps ?? Array.Empty<IProp>())
                prop.AddToProps(context.Props);*/
            
            context._fragment.Mount(_fragment?.FragmentNode ?? _mainNode);
            return context;
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
            
            /*foreach (IProp prop in context._fragment.DefaultProps ?? Array.Empty<IProp>())
                prop.AddToProps(context.Props);*/
            
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
        
        public TComponent _<TComponent, TProps>(
            TProps props,
            string key = null, 
            Action<BaseContext<TNode>> render = null,
            [CallerLineNumber] int callerLine = 0
        ) where TComponent : BaseFragment<TNode> where TProps : struct
        {
            int? hash = key?.GetHashCode() ?? callerLine.GetHashCode();

            BaseContext<TNode> child = Child<TComponent, TProps>(hash);
            SetNodeAsLastSibling(child._fragment.FragmentRoot);
            child.SetRender(render);
            
            PropList<TProps> propList = (PropList<TProps>) child.PropList;
            propList.Set(props);
            
            child.PropList.IsDirty = true;
            child.Traverse();
            
            return (TComponent) child._fragment;
        }
        
        public TComponent _<TComponent>(
            string key = null, 
            Action<BaseContext<TNode>> render = null, 
            [CallerLineNumber] int callerLine = 0
        ) where TComponent : BaseFragment<TNode>
        {
            int? hash = key?.GetHashCode() ?? callerLine.GetHashCode();

            BaseContext<TNode> child = Child<TComponent>(hash);
            SetNodeAsLastSibling(child._fragment.FragmentRoot);
            child.SetRender(render);

            child.PropList.IsDirty = true;
            child.Traverse();
            
            return (TComponent) child._fragment;
        }
        
        protected abstract void SetNodeAsLastSibling(TNode node);
    }
}