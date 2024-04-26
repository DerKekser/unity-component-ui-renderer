using System;
using UnityEngine;

namespace Scenes.Kekser.ComponentUI
{
    public class UIRenderer
    {
        private Context _context;
        
        private Action<Context> _render;
        
        public void Render(Action<Context> render)
        {
            _context = new Context(null);
            _render = render;
        }

        public void Update()
        {
            _context.Props.Set("ScreenWidth", Screen.width);
            _context.Props.Set("ScreenHeight", Screen.height);
            _context.Traverse(_render);
        }
    }
}