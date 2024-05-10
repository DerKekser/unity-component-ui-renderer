using System;
using UnityEngine;

namespace Kekser.ComponentSystem.StyleGenerator
{
    [CreateAssetMenu(fileName = "StyleGeneratorSettings", menuName = "Kekser/Style Generator Settings")]
    public class StyleGeneratorSettings : ScriptableObject
    {
        [SerializeField]
        private string[] _lookUpPaths = new string[0];
        [SerializeField]
        private string[] _safeList = new string[0];
        
        public string[] LookUpPaths => _lookUpPaths;
        public string[] SafeList => _safeList;

        private void Reset()
        {
#if UNITY_EDITOR
            _lookUpPaths = new[]
            {
                UnityEditor.AssetDatabase.GetAssetPath(this)
            };
            _safeList = Array.Empty<string>();
#endif
        }
    }
}