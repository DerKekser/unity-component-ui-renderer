using System;
using System.Collections.Generic;
using UnityEditor;
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
                
                if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(_resources[i], out string guid, out long localId))
                {
                    _resourcePaths[i] = $"{guid}-{localId}";
                }
                else
                {
                    string path = UnityEditor.AssetDatabase.GetAssetPath(_resources[i]);
                    _resourcePaths[i] = UnityEditor.AssetDatabase.AssetPathToGUID(path);
                }
            }
#endif
        }

        private void OnValidate()
        {
            UpdatePaths();
        }
    }
}