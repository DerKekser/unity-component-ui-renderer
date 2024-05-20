using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.StateSystem;

namespace Kekser.ComponentSystem.ComponentBase
{
    public class BaseContextProvider<TNode>: BaseFragment<TNode, NoProps>, IContextProvider<TNode> where TNode: class, new()
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