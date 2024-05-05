using System.Collections.Generic;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Object = UnityEngine.Object;

namespace Kekser.ComponentSystem.ComponentBase.Extension.ResourceManagement
{
    public class ResourceProps
    {
        public ObligatoryValue<ResourceDatabase> resources { get; set; } = new();
    }
    
    public sealed class ResourceProvider<TNode>: BaseProvider<TNode, ResourceProps> where TNode: class, new()
    {
        private Dictionary<string, Object> _resources = new Dictionary<string, Object>();
        
        public void UpdateResources(ResourceDatabase resources)
        {
            if (resources == null)
                return;
            
            _resources = resources.GetResources();
        }
        
        public T GetResource<T>(string key) where T : Object
        {
            if (key.Contains("@"))
            {
                string[] split = key.Split('@');
                key = split[0];
            }
            
            if (!_resources.ContainsKey(key))
                return null;
            
            return _resources[key] as T;
        }
        
        public override void OnRender()
        {
            UpdateResources(OwnProps.resources);
            
            Children();
        }
    }
}