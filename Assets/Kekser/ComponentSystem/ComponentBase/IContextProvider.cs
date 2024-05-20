using System;

namespace Kekser.ComponentSystem.ComponentBase
{
    public interface IContextProvider<TNode> : IFragment<TNode> where TNode : class, new()
    {
        void RegisterDirty(Action action);
    }
}