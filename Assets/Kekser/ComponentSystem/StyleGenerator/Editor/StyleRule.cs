#if UNITY_EDITOR
using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Kekser.ComponentSystem.StyleGenerator
{
    public class StyleRule
    {
        private const string _pseudoClassPattern = @"((\[(.+)]|(.+)):)?";
        private const string _valuePattern = @"-(\[(.+)]|([0-9]+))";
        
        private string _identifier;
        private string _unit;
        private bool _hasValue;
        private Func<string, string> _callback;

        private Regex _pattern;
        
        public StyleRule(string identifier, string unit, bool hasValue, Func<string, string> callback)
        {
            _identifier = identifier;
            _unit = unit;
            _hasValue = hasValue;
            _callback = callback;
            
            _pattern = new Regex($"^{_pseudoClassPattern}{_identifier}{(_hasValue ? _valuePattern : "")}$");
        }
        
        public string CleanupClassName(string className)
        {
            // TODO: escape some special characters instead of replacing them with "_"
            return Regex.Replace(className, @"[^a-zA-Z0-9-_]", @"_");
        }

        public bool IsMatch(string className)
        {
            return _pattern.IsMatch(className);
        }
        
        public string ApplyStyle(string className)
        {
            Match matches = _pattern.Match(className);
            
            string pseudo = matches.Groups[3].Success ? matches.Groups[3].Value : matches.Groups[4].Value;
            string value = _hasValue ? (matches.Groups[6].Success ? matches.Groups[6].Value : $"{matches.Groups[7].Value}{_unit}") : "";
            
            pseudo = pseudo.Replace("_", " ").Replace("&", "");
            value = value.Replace("_", " ");
            
            className = CleanupClassName(className) + (matches.Groups[4].Success ? $":{pseudo}" : pseudo);

            return $".{className} {{ {_callback(value)}; }}";
        }
    }
}
#endif