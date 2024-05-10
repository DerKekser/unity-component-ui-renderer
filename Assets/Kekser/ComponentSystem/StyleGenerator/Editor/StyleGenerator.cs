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
            // TODO: add support for deep selectors like [&_*] or [&>*]
            // TODO: add support for pseudo classes like :hover or :active
            // TODO: add support for different color formats like rgb
            
            new StyleRule(@"m-([0-9]+)", (match, className) => $".{className} {{ margin: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"m-\[(.+)]", (match, className) => $".{className} {{ margin: {match.Groups[2].Value}; }}"),
            new StyleRule(@"mt-([0-9]+)", (match, className) => $".{className} {{ margin-top: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"mt-\[(.+)]", (match, className) => $".{className} {{ margin-top: {match.Groups[2].Value}; }}"),
            new StyleRule(@"mr-([0-9]+)", (match, className) => $".{className} {{ margin-right: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"mr-\[(.+)]", (match, className) => $".{className} {{ margin-right: {match.Groups[2].Value}; }}"),
            new StyleRule(@"mb-([0-9]+)", (match, className) => $".{className} {{ margin-bottom: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"mb-\[(.+)]", (match, className) => $".{className} {{ margin-bottom: {match.Groups[2].Value}; }}"),
            new StyleRule(@"ml-([0-9]+)", (match, className) => $".{className} {{ margin-left: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"ml-\[(.+)]", (match, className) => $".{className} {{ margin-left: {match.Groups[2].Value}; }}"),
            
            new StyleRule(@"p-([0-9]+)", (match, className) => $".{className} {{ padding: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"p-\[(.+)]", (match, className) => $".{className} {{ padding: {match.Groups[2].Value}; }}"),
            new StyleRule(@"pt-([0-9]+)", (match, className) => $".{className} {{ padding-top: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"pt-\[(.+)]", (match, className) => $".{className} {{ padding-top: {match.Groups[2].Value}; }}"),
            new StyleRule(@"pr-([0-9]+)", (match, className) => $".{className} {{ padding-right: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"pr-\[(.+)]", (match, className) => $".{className} {{ padding-right: {match.Groups[2].Value}; }}"),
            new StyleRule(@"pb-([0-9]+)", (match, className) => $".{className} {{ padding-bottom: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"pb-\[(.+)]", (match, className) => $".{className} {{ padding-bottom: {match.Groups[2].Value}; }}"),
            new StyleRule(@"pl-([0-9]+)", (match, className) => $".{className} {{ padding-left: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"pl-\[(.+)]", (match, className) => $".{className} {{ padding-left: {match.Groups[2].Value}; }}"),
            
            new StyleRule(@"w-([0-9]+)", (match, className) => $".{className} {{ width: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"w-\[(.+)]", (match, className) => $".{className} {{ width: {match.Groups[2].Value}; }}"),
            new StyleRule(@"h-([0-9]+)", (match, className) => $".{className} {{ height: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"h-\[(.+)]", (match, className) => $".{className} {{ height: {match.Groups[2].Value}; }}"),
            
            new StyleRule(@"max-w-([0-9]+)", (match, className) => $".{className} {{ max-width: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"max-w-\[(.+)]", (match, className) => $".{className} {{ max-width: {match.Groups[2].Value}; }}"),
            new StyleRule(@"max-h-([0-9]+)", (match, className) => $".{className} {{ max-height: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"max-h-\[(.+)]", (match, className) => $".{className} {{ max-height: {match.Groups[2].Value}; }}"),
            new StyleRule(@"min-w-([0-9]+)", (match, className) => $".{className} {{ min-width: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"min-w-\[(.+)]", (match, className) => $".{className} {{ min-width: {match.Groups[2].Value}; }}"),
            new StyleRule(@"min-h-([0-9]+)", (match, className) => $".{className} {{ min-height: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"min-h-\[(.+)]", (match, className) => $".{className} {{ min-height: {match.Groups[2].Value}; }}"),
            
            new StyleRule(@"t-([0-9]+)", (match, className) => $".{className} {{ top: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"t-\[(.+)]", (match, className) => $".{className} {{ top: {match.Groups[2].Value}; }}"),
            new StyleRule(@"r-([0-9]+)", (match, className) => $".{className} {{ right: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"r-\[(.+)]", (match, className) => $".{className} {{ right: {match.Groups[2].Value}; }}"),
            new StyleRule(@"b-([0-9]+)", (match, className) => $".{className} {{ bottom: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"b-\[(.+)]", (match, className) => $".{className} {{ bottom: {match.Groups[2].Value}; }}"),
            new StyleRule(@"l-([0-9]+)", (match, className) => $".{className} {{ left: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"l-\[(.+)]", (match, className) => $".{className} {{ left: {match.Groups[2].Value}; }}"),
            
            new StyleRule(@"translate-([0-9]+)", (match, className) => $".{className} {{ translate: {match.Groups[2].Value}px {match.Groups[2].Value}px; }}"),
            new StyleRule(@"translate-\[(.+)]", (match, className) => $".{className} {{ translate: {match.Groups[2].Value} {match.Groups[2].Value}; }}"),
            new StyleRule(@"translateX-([0-9]+)", (match, className) => $".{className} {{ translate: {match.Groups[2].Value}px 0px; }}"),
            new StyleRule(@"translateX-\[(.+)]", (match, className) => $".{className} {{ translate: {match.Groups[2].Value} 0px; }}"),
            new StyleRule(@"translateY-([0-9]+)", (match, className) => $".{className} {{ translate: 0px {match.Groups[2].Value}px; }}"),
            new StyleRule(@"translateY-\[(.+)]", (match, className) => $".{className} {{ translate: 0px {match.Groups[2].Value}; }}"),
            
            new StyleRule(@"bg-([a-zA-Z]+)", (match, className) => $".{className} {{ background-color: {match.Groups[2].Value}; }}"),
            new StyleRule(@"bg-\[(.+)]", (match, className) =>
            {
                string raw = match.Groups[2].Value;
                string[] parts = raw.Split('@');
                
                if (ColorUtility.TryParseHtmlString(raw, out Color color) || parts.Length == 0)
                    return $".{className} {{ background-color: {raw}; }}";
                
                string assetPath = parts[0].Replace("%20", " ");
                string fileName = parts.Length > 1 ? parts[1].Replace("%20", " ") : null;
                
                Object[] assets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
                if (assets.Length > 0)
                {
                    string guid = AssetDatabase.AssetPathToGUID(assetPath);
                    Object fileObj = assets.FirstOrDefault(x => x.name == fileName);
                    
                    // TODO: add handling for fileID of single asset and support for type
                    if (fileName == null || fileObj == null)
                        return $".{className} {{ background-image: url('project://database/{assetPath.Replace(" ", "%20")}?fileID=2800000&guid={guid}'); }}";
                    
                    AssetDatabase.TryGetGUIDAndLocalFileIdentifier(fileObj, out string fileId, out long _);
                    
                    return $".{className} {{ background-image: url('project://database/{assetPath.Replace(" ", "%20")}?fileID={fileId}&guid={guid}&type=3#{fileName.Replace(" ", "%20")}'); }}";
                }
                
                return $".{className} {{ background-image: url('{raw}'); }}";
            }),
            new StyleRule(@"color-([a-zA-Z]+)", (match, className) => $".{className} {{ color: {match.Groups[2].Value}; }}"),
            new StyleRule(@"color-\[(.+)]", (match, className) => $".{className} {{ color: {match.Groups[2].Value}; }}"),
            
            new StyleRule(@"font-([0-9]+)", (match, className) => $".{className} {{ font-size: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"font-\[(.+)]", (match, className) => $".{className} {{ font-size: {match.Groups[2].Value}; }}"),
            
            new StyleRule(@"absolute", (match, className) => $".{className} {{ position: absolute; }}"),
            new StyleRule(@"relative", (match, className) => $".{className} {{ position: relative; }}"),
            
            new StyleRule(@"flex", (match, className) => $".{className} {{ display: flex; }}"),
            new StyleRule(@"none", (match, className) => $".{className} {{ display: none; }}"),
            
            new StyleRule(@"flex-([0-9]+)", (match, className) => $".{className} {{ flex: {match.Groups[2].Value}; }}"),
            new StyleRule(@"flex-\[(.+)]", (match, className) => $".{className} {{ flex: {match.Groups[2].Value}; }}"),
            new StyleRule(@"flex-grow-([0-9]+)", (match, className) => $".{className} {{ flex-grow: {match.Groups[2].Value}; }}"),
            new StyleRule(@"flex-grow-\[(.+)]", (match, className) => $".{className} {{ flex-grow: {match.Groups[2].Value}; }}"),
            new StyleRule(@"flex-shrink-([0-9]+)", (match, className) => $".{className} {{ flex-shrink: {match.Groups[2].Value}; }}"),
            new StyleRule(@"flex-shrink-\[(.+)]", (match, className) => $".{className} {{ flex-shrink: {match.Groups[2].Value}; }}"),
            new StyleRule(@"flex-basis-([0-9]+)", (match, className) => $".{className} {{ flex-basis: {match.Groups[2].Value}px; }}"),
            new StyleRule(@"flex-basis-\[(.+)]", (match, className) => $".{className} {{ flex-basis: {match.Groups[2].Value}; }}"),
            
            new StyleRule(@"flex-row", (match, className) => $".{className} {{ flex-direction: row; }}"),
            new StyleRule(@"flex-row-reverse", (match, className) => $".{className} {{ flex-direction: row-reverse; }}"),
            new StyleRule(@"flex-column", (match, className) => $".{className} {{ flex-direction: column; }}"),
            new StyleRule(@"flex-column-reverse", (match, className) => $".{className} {{ flex-direction: column-reverse; }}"),
            
            new StyleRule(@"flex-wrap", (match, className) => $".{className} {{ flex-wrap: wrap; }}"),
            new StyleRule(@"flex-wrap-reverse", (match, className) => $".{className} {{ flex-wrap: wrap-reverse; }}"),
            new StyleRule(@"flex-nowrap", (match, className) => $".{className} {{ flex-wrap: nowrap; }}"),
            
            new StyleRule(@"justify-start", (match, className) => $".{className} {{ justify-content: flex-start; }}"),
            new StyleRule(@"justify-end", (match, className) => $".{className} {{ justify-content: flex-end; }}"),
            new StyleRule(@"justify-center", (match, className) => $".{className} {{ justify-content: center; }}"),
            new StyleRule(@"justify-between", (match, className) => $".{className} {{ justify-content: space-between; }}"),
            new StyleRule(@"justify-around", (match, className) => $".{className} {{ justify-content: space-around; }}"),
            
            new StyleRule(@"items-auto", (match, className) => $".{className} {{ align-items: auto; }}"),
            new StyleRule(@"items-start", (match, className) => $".{className} {{ align-items: flex-start; }}"),
            new StyleRule(@"items-end", (match, className) => $".{className} {{ align-items: flex-end; }}"),
            new StyleRule(@"items-center", (match, className) => $".{className} {{ align-items: center; }}"),
            new StyleRule(@"items-stretch", (match, className) => $".{className} {{ align-items: stretch; }}"),
            
            new StyleRule(@"self-auto", (match, className) => $".{className} {{ align-self: auto; }}"),
            new StyleRule(@"self-start", (match, className) => $".{className} {{ align-self: flex-start; }}"),
            new StyleRule(@"self-end", (match, className) => $".{className} {{ align-self: flex-end; }}"),
            new StyleRule(@"self-center", (match, className) => $".{className} {{ align-self: center; }}"),
            new StyleRule(@"self-stretch", (match, className) => $".{className} {{ align-self: stretch; }}"),
            
            new StyleRule(@"align-auto", (match, className) => $".{className} {{ align-content: auto; }}"),
            new StyleRule(@"align-start", (match, className) => $".{className} {{ align-content: flex-start; }}"),
            new StyleRule(@"align-end", (match, className) => $".{className} {{ align-content: flex-end; }}"),
            new StyleRule(@"align-center", (match, className) => $".{className} {{ align-content: center; }}"),
            new StyleRule(@"align-stretch", (match, className) => $".{className} {{ align-content: stretch; }}"),
            
            new StyleRule(@"overflow-visible", (match, className) => $".{className} {{ overflow: visible; }}"),
            new StyleRule(@"overflow-hidden", (match, className) => $".{className} {{ overflow: hidden; }}"),
            
            new StyleRule(@"text-ellipsis", (match, className) => $".{className} {{ text-overflow: ellipsis; }}"),
            new StyleRule(@"text-wrap", (match, className) => $".{className} {{ white-space: wrap; }}"),
            new StyleRule(@"text-nowrap", (match, className) => $".{className} {{ white-space: nowrap; }}"),
        };
    }
}
#endif