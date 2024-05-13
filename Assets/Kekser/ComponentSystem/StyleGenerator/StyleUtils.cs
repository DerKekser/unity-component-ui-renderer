using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Kekser.ComponentSystem.StyleGenerator
{
    public static class StyleUtils
    {
        public static string Concat(params string[] values)
        {
            return string.Join(" ", values);
        }
        
        public static string CleanupClassName(string className)
        {
            Dictionary<string, string> specialChars = new()
            {
                {"&", "_and_"},
                {"*", "_st_"},
                {"<", "_lt_"},
                {">", "_gt_"},
            };

            string pattern = "[^a-zA-Z0-9-_]";
            string defaultReplacement = "_";
            return Regex.Replace(className, pattern, match => specialChars.TryGetValue(match.Value, out var replacement) ? replacement : defaultReplacement);
        }
        
    }
}