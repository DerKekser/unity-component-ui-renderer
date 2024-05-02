using System;
using UnityEngine;

namespace Kekser.ComponentSystem.ComponentBase
{
    public abstract class BaseRenderer<TNode> where TNode: class, new()
    {
        private BaseContext<TNode> _context;
        
        public bool Logging
        {
            get => _log;
            set => _log = value;
        }
        
        public void Render(Action<BaseContext<TNode>> render, TNode rootNode)
        {
            _context = CreateContext(rootNode);
            _context.SetRender(render);
        }
        
        public abstract BaseContext<TNode> CreateContext(TNode rootNode);

        public void Update()
        {
            _context.Traverse();
            Tick(_context);
        }
        
        protected virtual void Tick(BaseContext<TNode> ctx) { }
        
        private static bool _log = false;

        public static void Log(string message)
        {
            if (!_log)
                return;
            Debug.Log(message);
        }
    }
}