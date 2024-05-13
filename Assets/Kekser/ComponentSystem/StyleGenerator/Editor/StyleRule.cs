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
            Deg,
        }
        
        private readonly Dictionary<Unit, string> _unitMap = new()
        {
            {Unit.None, ""},
            {Unit.Pixel, "px"},
            {Unit.String, ""},
            {Unit.Int, ""},
            {Unit.Deg, "deg"},
        }; 
        
        private readonly Dictionary<Unit, string> _patternMap = new()
        {
            {Unit.None, @""},
            {Unit.Pixel, @"-(\[(.+)]|([0-9]+))"},
            {Unit.String, @"-(\[(.+)]|([0-9a-zA-Z-_]+))"},
            {Unit.Int, @"-(\[(.+)]|([0-9]+))"},
            {Unit.Deg, @"-(\[(.+)]|([0-9]+))"},
        };
        
        private const string _pseudoClassPattern = @"((\[(.+)]|(.+)):)?";
        private const string _importantPattern = @"(!)?";
        
        private string _identifier;
        private Unit _unit;
        private Func<string, string> _callback;

        private Regex _pattern;
        
        public StyleRule(string identifier, Unit unit, Func<string, string> callback)
        {
            _identifier = identifier;
            _unit = unit;
            _callback = callback;
            
            _pattern = new Regex($"^{_pseudoClassPattern}{_importantPattern}{_identifier}{_patternMap[unit]}$");
        }
        
        public bool IsMatch(string className)
        {
            return _pattern.IsMatch(className);
        }
        
        public string ApplyStyle(string className)
        {
            Match matches = _pattern.Match(className);
            
            string pseudo = matches.Groups[3].Success ? matches.Groups[3].Value : matches.Groups[4].Value;
            string value = _unit != Unit.None ? (matches.Groups[7].Success ? matches.Groups[7].Value : $"{matches.Groups[8].Value}{_unitMap[_unit]}") : "";
            
            string important = matches.Groups[5].Success ? " !important" : "";
            
            pseudo = pseudo.Replace("_", " ").Replace("&", "");
            value = value.Replace("_", " ");
            
            className = StyleUtils.CleanupClassName(className) + (matches.Groups[4].Success ? $":{pseudo}" : pseudo);

            return $".{className} {{ {_callback(value)}{important}; }}";
        }
    }
}
#endif