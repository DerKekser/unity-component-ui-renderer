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
        
        private static StyleRule[] _rules = new []
        {
            // TODO: add style reset

            new StyleRule("m", "px", true, value => $"margin: {value}"),
            new StyleRule("mt", "px", true, value => $"margin-top: {value}"),
            new StyleRule("mr", "px", true, value => $"margin-right: {value}"),
            new StyleRule("mb", "px", true, value => $"margin-bottom: {value}"),
            new StyleRule("ml", "px", true, value => $"margin-left: {value}"),
            
            new StyleRule("p", "px", true, value => $"padding: {value}"),
            new StyleRule("pt", "px", true, value => $"padding-top: {value}"),
            new StyleRule("pr", "px", true, value => $"padding-right: {value}"),
            new StyleRule("pb", "px", true, value => $"padding-bottom: {value}"),
            new StyleRule("pl", "px", true, value => $"padding-left: {value}"),
            
            new StyleRule("w", "px", true, value => $"width: {value}"),
            new StyleRule("h", "px", true, value => $"height: {value}"),
            
            new StyleRule("max-w", "px", true, value => $"max-width: {value}"),
            new StyleRule("max-h", "px", true, value => $"max-height: {value}"),
            new StyleRule("min-w", "px", true, value => $"min-width: {value}"),
            new StyleRule("min-h", "px", true, value => $"min-height: {value}"),
            
            new StyleRule("t", "px", true, value => $"top: {value}"),
            new StyleRule("r", "px", true, value => $"right: {value}"),
            new StyleRule("b", "px", true, value => $"bottom: {value}"),
            new StyleRule("l", "px", true, value => $"left: {value}"),
            
            new StyleRule("translate", "px", true, value => $"translate: {value}"),
            new StyleRule("translate-x", "px", true, value => $"translate: {value} 0px"),
            new StyleRule("translate-y", "px", true, value => $"translate: 0px {value}"),
            
            new StyleRule("bg", "", true, value =>
                {
                    value = value.Replace(" ", "_");
                    string[] parts = value.Split('@');
                    
                    if (ColorUtility.TryParseHtmlString(value, out Color color) || parts.Length == 0)
                        return $"background-color: {value}";

                    string assetPath = HttpUtility.UrlDecode(parts[0]);
                    string fileName = parts.Length > 1 ? HttpUtility.UrlDecode(parts[1]) : null;
                
                    Object[] assets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
                    if (assets.Length > 0)
                    {
                        string guid = AssetDatabase.AssetPathToGUID(assetPath);
                        Object fileObj = assets.FirstOrDefault(x => x.name == fileName);
                    
                        // TODO: add handling for fileID of single asset and support for type
                        if (fileName == null || fileObj == null)
                            return $"background-image: url('project://database/{HttpUtility.UrlPathEncode(assetPath)}?fileID=2800000&guid={guid}')";
                    
                        AssetDatabase.TryGetGUIDAndLocalFileIdentifier(fileObj, out string fileId, out long _);
                    
                        return $"background-image: url('project://database/{HttpUtility.UrlPathEncode(assetPath)}?fileID={fileId}&guid={guid}&type=3#{HttpUtility.UrlPathEncode(fileName)}')";
                    }
                
                    return $"background-image: url('{value}')";
                }),
            new StyleRule("color", "", true, value => $"color: {value}"),
            
            new StyleRule("font", "px", true, value => $"font-size: {value}"),
            
            new StyleRule("absolute", "", false, _ => "position: absolute"),
            new StyleRule("relative", "", false, _ => "position: relative"),
            
            new StyleRule("flex", "", false, _ => "display: flex"),
            new StyleRule("none", "", false, _ => "display: none"),
            
            new StyleRule("flex", "", true, value => $"flex: {value}"),
            new StyleRule("flex-grow", "", true, value => $"flex-grow: {value}"),
            new StyleRule("flex-shrink", "", true, value => $"flex-shrink: {value}"),
            new StyleRule("flex-basis", "", true, value => $"flex-basis: {value}"),
            
            new StyleRule("flex-row", "", false, _ => "flex-direction: row"),
            new StyleRule("flex-row-reverse", "", false, _ => "flex-direction: row-reverse"),
            new StyleRule("flex-column", "", false, _ => "flex-direction: column"),
            new StyleRule("flex-column-reverse", "", false, _ => "flex-direction: column-reverse"),
            
            new StyleRule("flex-wrap", "", false, _ => "flex-wrap: wrap"),
            new StyleRule("flex-wrap-reverse", "", false, _ => "flex-wrap: wrap-reverse"),
            new StyleRule("flex-nowrap", "", false, _ => "flex-wrap: nowrap"),
            
            new StyleRule("justify-start", "", false, _ => "justify-content: flex-start"),
            new StyleRule("justify-end", "", false, _ => "justify-content: flex-end"),
            new StyleRule("justify-center", "", false, _ => "justify-content: center"),
            new StyleRule("justify-between", "", false, _ => "justify-content: space-between"),
            new StyleRule("justify-around", "", false, _ => "justify-content: space-around"),
            
            new StyleRule("items-auto", "", false, _ => "align-items: auto"),
            new StyleRule("items-start", "", false, _ => "align-items: flex-start"),
            new StyleRule("items-end", "", false, _ => "align-items: flex-end"),
            new StyleRule("items-center", "", false, _ => "align-items: center"),
            new StyleRule("items-stretch", "", false, _ => "align-items: stretch"),
            
            new StyleRule("self-auto", "", false, _ => "align-self: auto"),
            new StyleRule("self-start", "", false, _ => "align-self: flex-start"),
            new StyleRule("self-end", "", false, _ => "align-self: flex-end"),
            new StyleRule("self-center", "", false, _ => "align-self: center"),
            new StyleRule("self-stretch", "", false, _ => "align-self: stretch"),
            
            new StyleRule("align-auto", "", false, _ => "align-content: auto"),
            new StyleRule("align-start", "", false, _ => "align-content: flex-start"),
            new StyleRule("align-end", "", false, _ => "align-content: flex-end"),
            new StyleRule("align-center", "", false, _ => "align-content: center"),
            new StyleRule("align-stretch", "", false, _ => "align-content: stretch"),
            
            new StyleRule("overflow-visible", "", false, _ => "overflow: visible"),
            new StyleRule("overflow-hidden", "", false, _ => "overflow: hidden"),
            
            new StyleRule("text-ellipsis", "", false, _ => "text-overflow: ellipsis"),
            new StyleRule("text-wrap", "", false, _ => "white-space: wrap"),
            new StyleRule("text-nowrap", "", false, _ => "white-space: nowrap"),
            
            // TODO: add support for deep selectors like [&_*] or [&>*]
            // TODO: add support for pseudo classes like :hover or :active
            // TODO: add support for different color formats like rgb
        };
    }
}
#endif