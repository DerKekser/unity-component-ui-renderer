using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Kekser.ComponentSystem.ComponentBase.Extension.ResourceManagement
{
    [CreateAssetMenu(fileName = "new ResourceDatabase", menuName = "Kekser/ComponentUI/ResourceDatabase")]
    public sealed class ResourceDatabase : ScriptableObject
    {
        [SerializeField]
        private List<Object> _resources;
        
        [HideInInspector]
        [SerializeField]
        private string[] _resourcePaths;
        
        public Dictionary<string, Object> GetResources()
        {
            Dictionary<string, Object> resources = new Dictionary<string, Object>();
            for (int i = 0; i < _resources.Count; i++)
            {
                if (_resources[i] == null)
                    continue;
                
                resources.Add(_resourcePaths[i], _resources[i]);
            }

            return resources;
        }
        
        public void AddResource(Object resource)
        {
            _resources.Add(resource);
            UpdatePaths();
        }
        
        public void RemoveResource(Object resource)
        {
            int index = _resources.IndexOf(resource);
            if (index == -1)
                return;
            
            _resources.RemoveAt(index);
            UpdatePaths();
        }

        private void UpdatePaths()
        {
#if UNITY_EDITOR
            _resourcePaths = new string[_resources.Count];
            for (int i = 0; i < _resources.Count; i++)
            {
                if (_resources[i] == null)
                    continue;
                
                string path = UnityEditor.AssetDatabase.GetAssetPath(_resources[i]);
                //cleanup path: change folder delimiter to and remove the "Assets/" prefix and the file extension
                path = path.Replace("\\", "/");
                if (path.StartsWith("Assets/"))
                    path = path.Substring("Assets/".Length);
                if (path.Contains("."))
                    path = path.Substring(0, path.LastIndexOf(".", StringComparison.Ordinal));
                if (!path.EndsWith(_resources[i].name))
                    path += "/" + _resources[i].name;
                _resourcePaths[i] = path;
            }
#endif
        }

        private void OnValidate()
        {
            UpdatePaths();
        }
    }
}