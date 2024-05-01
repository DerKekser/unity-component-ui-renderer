using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Kekser.ComponentUI.Extension.ResourceManagement
{
    public sealed class ResourceProvider: UIProvider
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
        
        public override void OnRender(Context ctx, Action<Context> children)
        {
            UpdateResources(Props.Get<ResourceDatabase>("resources"));
            
            children?.Invoke(ctx);
        }
    }
}