using System;
using UnityEngine;

namespace Kekser.ComponentSystem.ComponentBase
{
    public class RenderFragment<TNode, TProps> : BaseFragment<TNode, TProps> where TNode : class, new() where TProps : class, new()
    {
        public RenderFragment(TNode node)
        {
            _fragmentRoot = node;
            _fragmentNode = node;
        }
            
        public override void OnRender()
        {
            Children();
        }
    }

    public abstract class BaseRenderer<TNode> where TNode : class, new()
    {
        private BaseContext<TNode> _context;
        
        public bool Logging
        {
            get => _log;
            set => _log = value;
        }
        
        public void Render(Action<BaseContext<TNode>> render, TNode rootNode)
        {
            if (rootNode == null)
                throw new Exception("Root node is null");
            _context = CreateContext(rootNode);
            _context.SetRender(render);
        }
        
        public void Destroy()
        {
            _context.Destroy();
        }
        
        public abstract BaseContext<TNode> CreateContext(TNode rootNode);

        public void Update()
        {
            _context.Traverse(false);
            Tick(_context);
        }
        
        protected virtual void Tick(BaseContext<TNode> ctx) { }
        
        private static bool _log = false;

        public static void Log(Func<string> message)
        {
            if (!_log)
                return;
            Debug.Log(message());
        }
    }
}