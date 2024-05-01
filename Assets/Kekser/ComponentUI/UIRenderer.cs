using System;
using UnityEngine;
using UnityEngine.UIElements;

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
        
        public void Render(Action<Context> render, UIDocument uiDocument)
        {
            _context = new Context(uiDocument.rootVisualElement);
            _context.SetRender(render);
        }

        public void Update()
        {
            _context.Traverse();
            _context.Props.Set("screenWidth", Screen.width);
            _context.Props.Set("screenHeight", Screen.height);
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