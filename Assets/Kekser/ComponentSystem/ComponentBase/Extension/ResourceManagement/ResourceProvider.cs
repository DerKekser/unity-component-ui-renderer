using System.Collections.Generic;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Object = UnityEngine.Object;

namespace Kekser.ComponentSystem.ComponentBase.Extension.ResourceManagement
{
    public class ResourceProps
    {
        public ObligatoryValue<ResourceDatabase> resources { get; set; } = new();
    }
    
    public sealed class ResourceProvider<T>: BaseProvider<T, ResourceProps> where T: class, new()
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
            if (!_resources.ContainsKey(key))
                return null;
            
            return _resources[key] as T;
        }
        
        public override void OnRender(BaseContext<T> ctx)
        {
            UpdateResources(OwnProps.resources);
            
            Children(ctx);
        }
    }
}