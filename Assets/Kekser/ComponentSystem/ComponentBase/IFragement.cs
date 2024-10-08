﻿using Kekser.ComponentSystem.ComponentBase.PropSystem;
using UnityEngine.Scripting;

namespace Kekser.ComponentSystem.ComponentBase
{
    [RequireImplementors]
    public interface IFragment<TNode> where TNode: class, new()
    {
        TNode FragmentRoot { get; }
        TNode Node { get; }
        
        bool IsDirty { get; }
        
        void Mount(TNode parent);
        void Unmount();
        void Render();
        void SetContext(BaseContext<TNode> ctx);
        TParent GetParent<TParent>() where TParent : class, IFragment<TNode>;
    }
    
    public interface IFragment<TNode, TProps> : IFragment<TNode> where TNode: class, new() where TProps: class, new()
    {
        TProps Props { get; set; }
        TProps DefaultProps { get; }
    }
}