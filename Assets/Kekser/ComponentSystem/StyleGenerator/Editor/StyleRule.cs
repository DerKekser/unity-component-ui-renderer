#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Kekser.ComponentSystem.StyleGenerator
{
    public class StyleRule
    {
        public enum Unit
        {
            None,
            Pixel,
            String,
            Int,
        }
        
        private readonly Dictionary<Unit, string> _unitMap = new()
        {
            {Unit.None, ""},
            {Unit.Pixel, "px"},
            {Unit.String, ""},
            {Unit.Int, ""},
        }; 
        
        private readonly Dictionary<Unit, string> _patternMap = new()
        {
            {Unit.None, @""},
            {Unit.Pixel, @"-(\[(.+)]|([0-9]+))"},
            {Unit.String, @"-(\[(.+)]|([0-9a-zA-Z-_]+))"},
            {Unit.Int, @"-(\[(.+)]|([0-9]+))"},
        };
        
        private const string _pseudoClassPattern = @"((\[(.+)]|(.+)):)?";
        
        private string _identifier;
        private Unit _unit;
        private Func<string, string> _callback;

        private Regex _pattern;
        
        public StyleRule(string identifier, Unit unit, Func<string, string> callback)
        {
            _identifier = identifier;
            _unit = unit;
            _callback = callback;
            
            _pattern = new Regex($"^{_pseudoClassPattern}{_identifier}{_patternMap[unit]}$");
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
            string value = _unit != Unit.None ? (matches.Groups[6].Success ? matches.Groups[6].Value : $"{matches.Groups[7].Value}{_unitMap[_unit]}") : "";
            
            pseudo = pseudo.Replace("_", " ").Replace("&", "");
            value = value.Replace("_", " ");
            
            className = CleanupClassName(className) + (matches.Groups[4].Success ? $":{pseudo}" : pseudo);

            return $".{className} {{ {_callback(value)}; }}";
        }
    }
}
#endif