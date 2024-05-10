#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kekser.ComponentSystem.StyleGenerator
{
    [CustomEditor(typeof(StyleGeneratorSettings))]
    public class StyleGeneratorSettingsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            StyleGeneratorSettings settings = (StyleGeneratorSettings) target;
            
            if (GUILayout.Button("Generate Styles"))
                StyleGenerator.GenerateStyles(settings);
            
            List<string> lookUpPaths = new List<string>(settings.LookUpPaths);
            if (lookUpPaths.Count == 0)
                EditorGUILayout.HelpBox("No look up paths set", MessageType.Warning);
            if (lookUpPaths.Contains("Assets"))
                EditorGUILayout.HelpBox("Assets folder is too broad", MessageType.Warning);
            
            base.OnInspectorGUI();
        }
    }
}
#endif