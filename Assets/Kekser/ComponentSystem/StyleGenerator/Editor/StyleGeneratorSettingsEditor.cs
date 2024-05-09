using UnityEditor;
using UnityEngine;

namespace Kekser.ComponentSystem.StyleGenerator
{
    public class StyleGeneratorSettingsEditor
    {
        [MenuItem("Edit/Style Generator/Select Settings")]
        public static void SelectSettings()
        {
            if (GetSettings() == null) CreateSettings();
            Selection.activeObject = GetSettings();
        }
        
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            if (GetSettings() != null) return;
            CreateSettings();
        }
        
        private static void CreateSettings()
        {
            StyleGeneratorSettings settings = ScriptableObject.CreateInstance<StyleGeneratorSettings>();
            AssetDatabase.CreateAsset(settings, "Assets/Kekser/ComponentSystem/StyleGenerator/StyleGeneratorSettings.asset");
            AssetDatabase.SaveAssets();
        }
        
        public static StyleGeneratorSettings GetSettings()
        {
            return AssetDatabase.LoadAssetAtPath<StyleGeneratorSettings>("Assets/Kekser/ComponentSystem/StyleGenerator/StyleGeneratorSettings.asset");
        }
    }
}