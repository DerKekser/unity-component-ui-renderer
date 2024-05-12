#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using UnityEditor;
using UnityEngine;

namespace Kekser.ComponentSystem.StyleGenerator
{
    //https://docs.unity3d.com/Manual/UIE-uss-properties.html
    
    public static class StyleGenerator
    {
        [MenuItem("Edit/Style Generator/Generate Styles")]
        [UnityEditor.Callbacks.DidReloadScripts]
        public static void GenerateStyles()
        {
            EditorUtility.DisplayProgressBar("Generating Styles", "Extracting words from scripts", 0.0f);
            
            StyleGeneratorSettings[] settings = FindSettings();
            for (int i = 0; i < settings.Length; i++)
            {
                StyleGeneratorSettings setting = settings[i];
                EditorUtility.DisplayProgressBar("Generating Styles", $"Generating styles for {setting.name}", (float)i / settings.Length);
                GenerateStyles(setting);
            }
            
            EditorUtility.ClearProgressBar();
        }
        
        private static StyleGeneratorSettings[] FindSettings()
        {
            string[] guids = AssetDatabase.FindAssets("t:StyleGeneratorSettings");
            return guids.Select(guid => AssetDatabase.LoadAssetAtPath<StyleGeneratorSettings>(AssetDatabase.GUIDToAssetPath(guid))).ToArray();
        }
        
        public static void GenerateStyles(StyleGeneratorSettings settings)
        {
            string[] words = ExtractWords(settings.LookUpPaths, settings.SafeList);
         
            StringBuilder styleSheet = new StringBuilder();
            styleSheet.Append("TextInput { --unity-sync-text-editor-engine: true; }\n"); // We need to enable this so that the TextInput can be interacted with
            foreach (StyleRule rule in _rules)
            {
                foreach (string word in words)
                {
                    if (!rule.IsMatch(word)) continue;
                    
                    styleSheet.Append(rule.ApplyStyle(word));
                    styleSheet.Append('\n');
                }
            }
            
            string settingsPath = AssetDatabase.GetAssetPath(settings);
            string styleSheetPath = settingsPath.Replace(".asset", ".uss");
            System.IO.File.WriteAllText(styleSheetPath, styleSheet.ToString());
        }

        private static string[] ExtractStringsFromScripts(string[] paths)
        {
            List<string> strings = new List<string>();

            foreach (string searchPath in paths)
            {
                string[] guids = AssetDatabase.FindAssets("t:Script", new[] {searchPath});
                foreach (string guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    string text = System.IO.File.ReadAllText(path);
                
                    string[] lines = text.Split('\n');
                    foreach (string line in lines)
                    {
                        string[] stringsInLine = Regex.Matches(line, "\"([^\"]*)\"")
                            .Cast<Match>()
                            .Select(m => m.Groups[1].Value)
                            .ToArray();
                    
                        strings.AddRange(stringsInLine);
                    }
                }
            }
            
            return strings.Distinct().ToArray();
        }
        
        private static string[] ExtractWords(string[] paths, string[] knownWords)
        {
            string[] strings = ExtractStringsFromScripts(paths);
            List<string> words = new List<string>(knownWords);
            
            foreach (string str in strings)
            {
                string[] wordsInString = str.Split(' ');
                foreach (string word in wordsInString)
                {
                    if (!words.Contains(word))
                        words.Add(word);
                }
            }
            
            return words.Distinct().ToArray();
        }
        
        // TODO: color variables like red-500, blue-200, etc.
        
        private static StyleRule[] _rules = new []
        {
            new StyleRule("m", StyleRule.Unit.Pixel, value => $"margin: {value}"),
            new StyleRule("margin", StyleRule.Unit.Pixel, value => $"margin: {value}"),
            new StyleRule("mt", StyleRule.Unit.Pixel, value => $"margin-top: {value}"),
            new StyleRule("margin-top", StyleRule.Unit.Pixel, value => $"margin-top: {value}"),
            new StyleRule("mr", StyleRule.Unit.Pixel, value => $"margin-right: {value}"),
            new StyleRule("margin-right", StyleRule.Unit.Pixel, value => $"margin-right: {value}"),
            new StyleRule("mb", StyleRule.Unit.Pixel, value => $"margin-bottom: {value}"),
            new StyleRule("margin-bottom", StyleRule.Unit.Pixel, value => $"margin-bottom: {value}"),
            new StyleRule("ml", StyleRule.Unit.Pixel, value => $"margin-left: {value}"),
            new StyleRule("margin-left", StyleRule.Unit.Pixel, value => $"margin-left: {value}"),
            
            new StyleRule("p", StyleRule.Unit.Pixel, value => $"padding: {value}"),
            new StyleRule("padding", StyleRule.Unit.Pixel, value => $"padding: {value}"),
            new StyleRule("pt", StyleRule.Unit.Pixel, value => $"padding-top: {value}"),
            new StyleRule("padding-top", StyleRule.Unit.Pixel, value => $"padding-top: {value}"),
            new StyleRule("pr", StyleRule.Unit.Pixel, value => $"padding-right: {value}"),
            new StyleRule("padding-right", StyleRule.Unit.Pixel, value => $"padding-right: {value}"),
            new StyleRule("pb", StyleRule.Unit.Pixel, value => $"padding-bottom: {value}"),
            new StyleRule("padding-bottom", StyleRule.Unit.Pixel, value => $"padding-bottom: {value}"),
            new StyleRule("pl", StyleRule.Unit.Pixel, value => $"padding-left: {value}"),
            new StyleRule("padding-left", StyleRule.Unit.Pixel, value => $"padding-left: {value}"),
            
            new StyleRule("w", StyleRule.Unit.Pixel, value => $"width: {value}"),
            new StyleRule("width", StyleRule.Unit.Pixel, value => $"width: {value}"),
            new StyleRule("h", StyleRule.Unit.Pixel, value => $"height: {value}"),
            new StyleRule("height", StyleRule.Unit.Pixel, value => $"height: {value}"),
            
            new StyleRule("max-w", StyleRule.Unit.Pixel, value => $"max-width: {value}"),
            new StyleRule("max-width", StyleRule.Unit.Pixel, value => $"max-width: {value}"),
            new StyleRule("max-h", StyleRule.Unit.Pixel, value => $"max-height: {value}"),
            new StyleRule("max-height", StyleRule.Unit.Pixel, value => $"max-height: {value}"),
            new StyleRule("min-w", StyleRule.Unit.Pixel, value => $"min-width: {value}"),
            new StyleRule("min-width", StyleRule.Unit.Pixel, value => $"min-width: {value}"),
            new StyleRule("min-h", StyleRule.Unit.Pixel, value => $"min-height: {value}"),
            new StyleRule("min-height", StyleRule.Unit.Pixel, value => $"min-height: {value}"),
            
            new StyleRule("t", StyleRule.Unit.Pixel, value => $"top: {value}"),
            new StyleRule("top", StyleRule.Unit.Pixel, value => $"top: {value}"),
            new StyleRule("r", StyleRule.Unit.Pixel, value => $"right: {value}"),
            new StyleRule("right", StyleRule.Unit.Pixel, value => $"right: {value}"),
            new StyleRule("b", StyleRule.Unit.Pixel, value => $"bottom: {value}"),
            new StyleRule("bottom", StyleRule.Unit.Pixel, value => $"bottom: {value}"),
            new StyleRule("l", StyleRule.Unit.Pixel, value => $"left: {value}"),
            new StyleRule("left", StyleRule.Unit.Pixel, value => $"left: {value}"),
            
            new StyleRule("translate", StyleRule.Unit.Pixel, value => $"translate: {value}"),
            new StyleRule("translate-x", StyleRule.Unit.Pixel, value => $"translate: {value} 0px"),
            new StyleRule("translate-y", StyleRule.Unit.Pixel, value => $"translate: 0px {value}"),
            
            new StyleRule("rotate", StyleRule.Unit.Deg, value => $"rotate: {value}"),
            new StyleRule("scale", StyleRule.Unit.String, value => $"scale: {value}"),
            
            new StyleRule("origin", StyleRule.Unit.String, value => $"transform-origin: {value}"),
            
            new StyleRule("bg", StyleRule.Unit.String, value =>
                {
                    value = value.Replace(" ", "_");
                    if (ColorUtility.TryParseHtmlString(value, out Color color))
                        return $"background-color: {value}";
                    return $"background-image: url('{value}')";
                }),
            new StyleRule("background", StyleRule.Unit.String, value =>
                {
                    value = value.Replace(" ", "_");
                    if (ColorUtility.TryParseHtmlString(value, out Color color))
                        return $"background-color: {value}";
                    return $"background-image: url('{value}')";
                }),
                
            new StyleRule("color", StyleRule.Unit.String, value => $"color: {value}"),
            
            new StyleRule("font", StyleRule.Unit.Pixel, value => $"font-size: {value}"),
            
            new StyleRule("unity-font", StyleRule.Unit.String, value =>
            {
                value = value.Replace(" ", "_");
                return $"-unity-font-definition: url('{value}')";
            }),
            
            new StyleRule("rounded", StyleRule.Unit.Pixel, value => $"border-radius: {value}"),
            new StyleRule("rounded-top", StyleRule.Unit.Pixel, value => $"border-top-left-radius: {value}; border-top-right-radius: {value}"),
            new StyleRule("rounded-right", StyleRule.Unit.Pixel, value => $"border-top-right-radius: {value}; border-bottom-right-radius: {value}"),
            new StyleRule("rounded-bottom", StyleRule.Unit.Pixel, value => $"border-bottom-right-radius: {value}; border-bottom-left-radius: {value}"),
            new StyleRule("rounded-left", StyleRule.Unit.Pixel, value => $"border-top-left-radius: {value}; border-bottom-left-radius: {value}"),
            new StyleRule("rounded-tl", StyleRule.Unit.Pixel, value => $"border-top-left-radius: {value}"),
            new StyleRule("rounded-tr", StyleRule.Unit.Pixel, value => $"border-top-right-radius: {value}"),
            new StyleRule("rounded-br", StyleRule.Unit.Pixel, value => $"border-bottom-right-radius: {value}"),
            new StyleRule("rounded-bl", StyleRule.Unit.Pixel, value => $"border-bottom-left-radius: {value}"),
            
            new StyleRule("absolute", StyleRule.Unit.None, _ => "position: absolute"),
            new StyleRule("relative", StyleRule.Unit.None, _ => "position: relative"),
            
            new StyleRule("flex", StyleRule.Unit.None, _ => "display: flex"),
            new StyleRule("none", StyleRule.Unit.None, _ => "display: none"),
            
            new StyleRule("visible", StyleRule.Unit.None, _ => "visibility: visible"),
            new StyleRule("hidden", StyleRule.Unit.None, _ => "visibility: hidden"),
            
            new StyleRule("flex", StyleRule.Unit.Int, value => $"flex: {value}"),
            new StyleRule("flex-grow", StyleRule.Unit.Int, value => $"flex-grow: {value}"),
            new StyleRule("flex-shrink", StyleRule.Unit.Int, value => $"flex-shrink: {value}"),
            new StyleRule("flex-basis", StyleRule.Unit.Int, value => $"flex-basis: {value}"),
            
            new StyleRule("flex-row", StyleRule.Unit.None, _ => "flex-direction: row"),
            new StyleRule("flex-row-reverse", StyleRule.Unit.None, _ => "flex-direction: row-reverse"),
            new StyleRule("flex-column", StyleRule.Unit.None, _ => "flex-direction: column"),
            new StyleRule("flex-column-reverse", StyleRule.Unit.None, _ => "flex-direction: column-reverse"),
            
            new StyleRule("flex-wrap", StyleRule.Unit.None, _ => "flex-wrap: wrap"),
            new StyleRule("flex-wrap-reverse", StyleRule.Unit.None, _ => "flex-wrap: wrap-reverse"),
            new StyleRule("flex-nowrap", StyleRule.Unit.None, _ => "flex-wrap: nowrap"),
            
            new StyleRule("justify-start", StyleRule.Unit.None, _ => "justify-content: flex-start"),
            new StyleRule("justify-end", StyleRule.Unit.None, _ => "justify-content: flex-end"),
            new StyleRule("justify-center", StyleRule.Unit.None, _ => "justify-content: center"),
            new StyleRule("justify-between", StyleRule.Unit.None, _ => "justify-content: space-between"),
            new StyleRule("justify-around", StyleRule.Unit.None, _ => "justify-content: space-around"),
            
            new StyleRule("items-auto", StyleRule.Unit.None, _ => "align-items: auto"),
            new StyleRule("items-start", StyleRule.Unit.None, _ => "align-items: flex-start"),
            new StyleRule("items-end", StyleRule.Unit.None, _ => "align-items: flex-end"),
            new StyleRule("items-center", StyleRule.Unit.None, _ => "align-items: center"),
            new StyleRule("items-stretch", StyleRule.Unit.None, _ => "align-items: stretch"),
            
            new StyleRule("self-auto", StyleRule.Unit.None, _ => "align-self: auto"),
            new StyleRule("self-start", StyleRule.Unit.None, _ => "align-self: flex-start"),
            new StyleRule("self-end", StyleRule.Unit.None, _ => "align-self: flex-end"),
            new StyleRule("self-center", StyleRule.Unit.None, _ => "align-self: center"),
            new StyleRule("self-stretch", StyleRule.Unit.None, _ => "align-self: stretch"),
            
            new StyleRule("align-auto", StyleRule.Unit.None, _ => "align-content: auto"),
            new StyleRule("align-start", StyleRule.Unit.None, _ => "align-content: flex-start"),
            new StyleRule("align-end", StyleRule.Unit.None, _ => "align-content: flex-end"),
            new StyleRule("align-center", StyleRule.Unit.None, _ => "align-content: center"),
            new StyleRule("align-stretch", StyleRule.Unit.None, _ => "align-content: stretch"),
            
            new StyleRule("overflow-visible", StyleRule.Unit.None, _ => "overflow: visible"),
            new StyleRule("overflow-hidden", StyleRule.Unit.None, _ => "overflow: hidden"),
            
            new StyleRule("text-align", StyleRule.Unit.String, value => $"-unity-text-align: {value}"),
            new StyleRule("text-shadow", StyleRule.Unit.String, value => $"text-shadow: {value}"),
                
            new StyleRule("text-ellipsis", StyleRule.Unit.None, _ => "text-overflow: ellipsis"),
            new StyleRule("text-wrap", StyleRule.Unit.None, _ => "white-space: wrap"),
            new StyleRule("text-nowrap", StyleRule.Unit.None, _ => "white-space: nowrap"),
        };
    }
}
#endif