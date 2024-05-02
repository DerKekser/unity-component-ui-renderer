using System;
using System.Collections.Generic;
using Kekser.ComponentSystem.ComponentBase;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Kekser.ComponentSystem.ComponentUI.Extension.ResourceManagement
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
        
        public override void OnRender(BaseContext<VisualElement> ctx, Action<BaseContext<VisualElement>> children)
        {
            UpdateResources(Props.Get<ResourceDatabase>("resources"));
            
            children?.Invoke(ctx);
        }
    }
}