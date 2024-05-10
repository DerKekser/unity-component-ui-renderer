#if UNITY_EDITOR
using System;
using System.Text.RegularExpressions;

namespace Kekser.ComponentSystem.StyleGenerator
{
    public class StyleRule
    {
        private string _className;
        private Func<Match, string, string> _style;
        
        public StyleRule(string className, Func<Match, string, string> style)
        {
            _className = className;
            _style = style;
        }
        
        public string CleanupClassName(string className)
        {
            return Regex.Replace(className, @"[^a-zA-Z0-9-_]", @"_");
        }
        
        public bool IsMatch(string className)
        {
            return Regex.IsMatch(className, _className);
        }

        public string ApplyStyle(string className)
        {
            Match matches = Regex.Match(className, _className);
            return _style(matches, CleanupClassName(className));
        }
    }
}
#endif