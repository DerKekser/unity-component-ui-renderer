using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.StateSystem;

namespace Kekser.ComponentSystem.ComponentBase
{
    public abstract class BaseContextProvider<TNode> : BaseContextProvider<TNode, NoProps> where TNode : class, new()
    {
        
    }
    
    public abstract class BaseContextProvider<TNode, TProps>: BaseFragment<TNode, TProps>, IContextProvider<TNode> where TNode: class, new() where TProps: class, new()
    {
        protected event Action _setDirty;
        
        public new State<T> UseState<T>(T defaultValue = default)
        {
            return new State<T>(() => _setDirty?.Invoke(), defaultValue);
        }
        
        public new TProvider UseContextProvider<TProvider>() where TProvider : class, IContextProvider<TNode>
        {
            TProvider provider = GetParent<TProvider>();
            
            provider.RegisterDirty(() => _setDirty?.Invoke());
            return provider;
        }

        public void RegisterDirty(Action action)
        {
            _setDirty += action;
        }
        
        protected override void OnRender()
        {
            Children();
        }
    }
}