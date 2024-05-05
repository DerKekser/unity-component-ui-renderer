#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Kekser.ComponentSystem.ComponentBase.Extension.ResourceManagement
{
    [CustomEditor(typeof(ResourceDatabase))]
    public class ResourceDatabaseEditor : Editor
    {
        private string _search = "";
        
        private static int CalcLevenshteinDistance(string a, string b)
        {
            if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b))
            {
                return 0;
            }

            if (string.IsNullOrEmpty(a))
            {
                return b.Length;
            }

            if (string.IsNullOrEmpty(b))
            {
                return a.Length;
            }

            int lengthA = a.Length;
            int lengthB = b.Length;
            var distances = new int[lengthA + 1][];
            for (int index = 0; index < lengthA + 1; index++)
            {
                distances[index] = new int[lengthB + 1];
            }

            for (int i = 0; i <= lengthA; distances[i][0] = i++);
            for (int j = 0; j <= lengthB; distances[0][j] = j++);

            for (int i = 1; i <= lengthA; i++)
            {
                for (int j = 1; j <= lengthB; j++)
                {
                    int cost = b[j - 1] == a[i - 1] ? 0 : 1;
            
                    distances[i][j] = Mathf.Min(
                        Mathf.Min(distances[i - 1][j] + 1, distances[i][j - 1] + 1),
                        distances[i - 1][j - 1] + cost
                    );
                }
            }

            return distances[lengthA][lengthB];
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ResourceDatabase database = target as ResourceDatabase;
            Dictionary<string, Object> resources = database.GetResources();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Resources", EditorStyles.boldLabel);
            _search = EditorGUILayout.TextField("Search", _search);
            int index = 0;
            foreach (KeyValuePair<string, Object> resource in resources.Where(r => string.IsNullOrEmpty(_search) || CalcLevenshteinDistance(r.Value.name, _search) < 3 || r.Value.name.Contains(_search)))
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(resource.Value.name);
                if (GUILayout.Button("Copy Path"))
                {
                    EditorGUIUtility.systemCopyBuffer = $"{resource.Key}@{resource.Value.name}";
                }
                EditorGUILayout.EndHorizontal();
                
                index++;
            }
            EditorGUILayout.EndVertical();
        }
    }
}
#endif