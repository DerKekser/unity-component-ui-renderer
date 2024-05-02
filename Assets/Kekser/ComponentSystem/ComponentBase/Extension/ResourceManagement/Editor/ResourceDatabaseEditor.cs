#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kekser.ComponentSystem.ComponentBase.Extension.ResourceManagement
{
    [CustomEditor(typeof(ResourceDatabase))]
    public class ResourceDatabaseEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ResourceDatabase database = target as ResourceDatabase;
            Dictionary<string, Object> resources = database.GetResources();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Resources", EditorStyles.boldLabel);
            int index = 0;
            foreach (KeyValuePair<string, Object> resource in resources)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(index.ToString(), GUILayout.Width(20));
                EditorGUILayout.LabelField(resource.Value.name);
                if (GUILayout.Button("Copy Path"))
                {
                    EditorGUIUtility.systemCopyBuffer = resource.Key;
                }
                EditorGUILayout.EndHorizontal();
                
                index++;
            }
            EditorGUILayout.EndVertical();
        }
    }
}
#endif