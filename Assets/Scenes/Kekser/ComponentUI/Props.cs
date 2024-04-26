using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Kekser.ComponentUI
{
    public struct Style
    {
        public string Width;
        public string Height;
        public string Top;
        public string Left;
        public string Right;
        public string Bottom;
    }
    
    public struct Event
    {
        public Action Callback;
    }
    
    public struct TextProp
    {
        public string Value;
    }
    
    public class Props
    {
        private Dictionary<string, object> _props = new Dictionary<string, object>();
        private bool _isDirty = true;

        public bool IsDirty
        {
            get => _isDirty;
            set => _isDirty = value;
        }
        
        public TProp Get<TProp>(string prop) where TProp : struct
        {
            if (_props.TryGetValue(prop, out object p))
                return (TProp) p;
            return default;
        }
        
        public void Set<TProp>(string prop, TProp p) where TProp : struct
        {
            if (_props.TryGetValue(prop, out object current) && current.Equals(p))
                return;
            _props[prop] = p;
            _isDirty = true;
        }
    }
}