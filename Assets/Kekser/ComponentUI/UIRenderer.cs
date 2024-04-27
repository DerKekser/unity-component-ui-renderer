using System;
using UnityEngine;

namespace Kekser.ComponentUI
{
    public sealed class UIRenderer
    {
        private Context _context;
        
        public bool Logging
        {
            get => _log;
            set => _log = value;
        }
        
        public void Render(Action<Context> render, Canvas canvas)
        {
            _context = new Context(canvas.transform);
            _context.SetRender(render);
        }

        public void Update()
        {
            _context.Props.Set("screenWidth", Screen.width);
            _context.Props.Set("screenHeight", Screen.height);
            _context.Traverse();
        }
        
        private static bool _log = false;

        public static void Log(string message)
        {
            if (!_log)
                return;
            Debug.Log(message);
        }
    }
}