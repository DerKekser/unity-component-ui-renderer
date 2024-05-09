using System;
using System.Reflection;

namespace Kekser.ComponentSystem.ComponentBase.PropSystem
{
    public class PropList<TProps>: IPropList<TProps> where TProps: class, new()
    {
        private static readonly PropertyInfo[] PropProperties = typeof(TProps).GetProperties();

        private TProps _props = new TProps();
        private bool _isDirty = true;
        
        public TProps Props => _props;
        
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
                        _isDirty = true;
                        break;
                    default:
                        if (propertyInfo.GetValue(_props) == propertyInfo.GetValue(props))
                            continue;
                        propertyInfo.SetValue(_props, propertyInfo.GetValue(props));
                        _isDirty = true;
                        break;
                }
            }
        }

        public TProps Get()
        {
            return _props;
        }

        public bool IsDirty 
        {
            get => _isDirty;
            set => _isDirty = value;
        }
    }
}