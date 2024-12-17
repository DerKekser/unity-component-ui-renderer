using System;
using System.Collections.Generic;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.StateSystem;
using Kekser.ComponentSystem.V2.ComponentBase.Components;

namespace Kekser.ComponentSystem.V2.ComponentBase
{
    public abstract class BaseFragment : IFragment
    {
        protected bool _isDirty = true;
        protected IFragmentContext _context;
        
        public State<T> UseState<T>(T defaultValue = default)
        {
            return new State<T>(() => _isDirty = true, defaultValue);
        }

        protected static IFragmentContext _(
            Func<IFragmentContext> func,
            int? key = null
        )
        {
            IFragmentContext context = func();
            context?.SetKey(key);
            return context;
        }

        protected static IFragmentContext _<T>(
            IEnumerable<T> children,
            Func<T, int, IFragmentContext> func
        )
        {
            IFragment fragment = new Fragment();
            FragmentContext context = new FragmentContext(fragment);
            List<IFragmentContext> childrenContexts = new List<IFragmentContext>();
            int index = 0;
            foreach (T child in children)
            {
                childrenContexts.Add(func(child, index));
                index++;
            }
            context.AddChildren(childrenContexts.ToArray());
            return context;
        }
        
        protected static IFragmentContext _<TFragment>(
            int? key = null,
            Action<IFragmentContext> reference = null 
        ) where TFragment : IFragment
        {
            TFragment fragment = (TFragment) Activator.CreateInstance(typeof(TFragment));
            FragmentContext context = new FragmentContext(fragment);
            context.SetKey(key);
            reference?.Invoke(context);
            return context;
        }
        
        protected static IFragmentContext _<TFragment, TComponentProps>(
            TComponentProps props,
            int? key = null,
            Action<IFragmentContext> reference = null
        ) where TFragment : IFragment<TComponentProps> where TComponentProps : class, new()
        {
            TFragment fragment = (TFragment) Activator.CreateInstance(typeof(TFragment));
            fragment.Props = props;
            FragmentContext context = new FragmentContext(fragment);
            context.SetKey(key);
            context.SetProps(props);
            reference?.Invoke(context);
            return context;
        }
        
        public IFragmentContext GetContext()
        {
            if (!_isDirty)
                return _context;
            
            _context = Render();
            _isDirty = false;
            return _context;
        }

        protected virtual IFragmentContext Render() => _<Children>(); // Defines the tree structure of the fragment and children
    }

    public abstract class BaseFragment<TProps> : BaseFragment, IFragment<TProps> where TProps : class, new()
    {
        protected PropList<TProps> _props;
        
        public TProps Props
        {
            get => _props.Props;
            set => _props.Set(value);
        }
        
        public BaseFragment()
        {
            _props = new PropList<TProps>(() => _isDirty = true);
        }
    }
}