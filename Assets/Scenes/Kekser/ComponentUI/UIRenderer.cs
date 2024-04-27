﻿using System;
using UnityEngine;

namespace Scenes.Kekser.ComponentUI
{
    public sealed class UIRenderer
    {
        private Context _context;
        
        private Action<Context> _render;
     
        public bool Logging
        {
            get => _log;
            set => _log = value;
        }
        
        public void Render(Action<Context> render, Canvas canvas)
        {
            _context = new Context(canvas.transform);
            _render = render;
        }

        public void Update()
        {
            _context.Props.Set("screenWidth", Screen.width);
            _context.Props.Set("screenHeight", Screen.height);
            _context.Traverse(_render);
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