using System;
using System.Reflection;

namespace Kekser.ComponentSystem.ComponentBase.PropSystem
{
    public class PropList<TProps>: IPropList<TProps> where TProps: class, new()
    {
        private static readonly PropertyInfo[] PropProperties = typeof(TProps).GetProperties();
        
        private TProps _props = new TProps();
        private Action _setDirty;
        
        public TProps Props => _props;

        public PropList(Action setDirty)
        {
            _setDirty = setDirty;
        }
        
        public void Set(TProps props)
        {
            foreach (PropertyInfo propertyInfo in PropProperties)
            {
                if (!propertyInfo.CanRead || !propertyInfo.CanWrite)
                    continue;

                switch (propertyInfo.GetValue(props))
                {
                    case IPropValue propValue:
                        if (!propValue.IsSet)
                            continue;
                        IPropValue propValue1 = (IPropValue) propertyInfo.GetValue(_props);
                        if (propValue1.Equals(propValue))
                            continue;
                        propValue1.TakeValue(propValue);
                        propertyInfo.SetValue(_props, propValue1);
                        _setDirty();
                        break;
                    default:
                        if (propertyInfo.GetValue(_props) == propertyInfo.GetValue(props))
                            continue;
                        propertyInfo.SetValue(_props, propertyInfo.GetValue(props));
                        _setDirty();
                        break;
                }
            }
        }

        public TProps Get()
        {
            return _props;
        }
    }
}