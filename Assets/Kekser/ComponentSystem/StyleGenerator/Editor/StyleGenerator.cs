﻿#if UNITY_EDITOR
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
        
        private static StyleRule[] _rules = new []
        {
            new StyleRule("m", StyleRule.Unit.Pixel, value => $"margin: {value}"),
            new StyleRule("mt", StyleRule.Unit.Pixel, value => $"margin-top: {value}"),
            new StyleRule("mr", StyleRule.Unit.Pixel, value => $"margin-right: {value}"),
            new StyleRule("mb", StyleRule.Unit.Pixel, value => $"margin-bottom: {value}"),
            new StyleRule("ml", StyleRule.Unit.Pixel, value => $"margin-left: {value}"),
            
            new StyleRule("p", StyleRule.Unit.Pixel, value => $"padding: {value}"),
            new StyleRule("pt", StyleRule.Unit.Pixel, value => $"padding-top: {value}"),
            new StyleRule("pr", StyleRule.Unit.Pixel, value => $"padding-right: {value}"),
            new StyleRule("pb", StyleRule.Unit.Pixel, value => $"padding-bottom: {value}"),
            new StyleRule("pl", StyleRule.Unit.Pixel, value => $"padding-left: {value}"),
            
            new StyleRule("w", StyleRule.Unit.Pixel, value => $"width: {value}"),
            new StyleRule("h", StyleRule.Unit.Pixel, value => $"height: {value}"),
            
            new StyleRule("max-w", StyleRule.Unit.Pixel, value => $"max-width: {value}"),
            new StyleRule("max-h", StyleRule.Unit.Pixel, value => $"max-height: {value}"),
            new StyleRule("min-w", StyleRule.Unit.Pixel, value => $"min-width: {value}"),
            new StyleRule("min-h", StyleRule.Unit.Pixel, value => $"min-height: {value}"),
            
            new StyleRule("t", StyleRule.Unit.Pixel, value => $"top: {value}"),
            new StyleRule("r", StyleRule.Unit.Pixel, value => $"right: {value}"),
            new StyleRule("b", StyleRule.Unit.Pixel, value => $"bottom: {value}"),
            new StyleRule("l", StyleRule.Unit.Pixel, value => $"left: {value}"),
            
            new StyleRule("translate", StyleRule.Unit.Pixel, value => $"translate: {value}"),
            new StyleRule("translate-x", StyleRule.Unit.Pixel, value => $"translate: {value} 0px"),
            new StyleRule("translate-y", StyleRule.Unit.Pixel, value => $"translate: 0px {value}"),
            
            new StyleRule("bg", StyleRule.Unit.String, value =>
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
            new StyleRule("color", StyleRule.Unit.String, value => $"color: {value}"),
            
            new StyleRule("font", StyleRule.Unit.Pixel, value => $"font-size: {value}"),
            
            new StyleRule("absolute", StyleRule.Unit.None, _ => "position: absolute"),
            new StyleRule("relative", StyleRule.Unit.None, _ => "position: relative"),
            
            new StyleRule("flex", StyleRule.Unit.None, _ => "display: flex"),
            new StyleRule("none", StyleRule.Unit.None, _ => "display: none"),
            
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
            
            new StyleRule("text-ellipsis", StyleRule.Unit.None, _ => "text-overflow: ellipsis"),
            new StyleRule("text-wrap", StyleRule.Unit.None, _ => "white-space: wrap"),
            new StyleRule("text-nowrap", StyleRule.Unit.None, _ => "white-space: nowrap"),
        };
    }
}
#endif