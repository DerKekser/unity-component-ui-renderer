using UnityEngine;

namespace Kekser.ComponentSystem.StyleGenerator
{
    public class StyleGeneratorSettings : ScriptableObject
    {
        [SerializeField]
        private string[] _lookUpPaths = new string[2] {"Assets\\Examples", "Assets\\Kekser"};
        [SerializeField]
        private string[] _addClasses = new string[0];
        
        public string[] LookUpPaths => _lookUpPaths;
        public string[] AddClasses => _addClasses;
    }
}