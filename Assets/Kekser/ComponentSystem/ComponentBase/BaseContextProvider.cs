using System;
using Kekser.ComponentSystem.ComponentBase.PropSystem;

namespace Kekser.ComponentSystem.ComponentBase
{
    public class ContextProviderProps<TProps> where TProps: class, new()
    {
        public ObligatoryValue<TProps> value { get; set; } = new();
    }
    
    public class BaseContextProvider<TNode, TProps>: BaseFragment<TNode, ContextProviderProps<TProps>>, IContextProvider<TNode> where TNode: class, new() where TProps: class, new()
    {
        protected PropList<TProps> _propList;
        
        protected event Action _setDirty;

        public TProps ProviderProps
        {
            get => _propList.Props;
            set => _propList.Set(value);
        }
        
        public BaseContextProvider()
        {
            _propList = new PropList<TProps>(() => _setDirty?.Invoke());
        }
        
        public void RegisterDirty(Action action)
        {
            _setDirty += action;
        }
        
        protected override void OnRender()
        {
            _propList.Set(Props.value);
            Children();
        }
    }
}