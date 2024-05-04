using System;
using System.Reflection;
using UnityEngine;

namespace Kekser.ComponentSystem.ComponentBase.PropSystem.Rework
{
    public class PropList<TProps>: IPropList where TProps: struct
    {
        private TProps _props;
        private bool _isDirty = true;
        
        public TProps Props => _props;
        
        public void Set(TProps props)
        {
            PropertyInfo[] propertyInfos = typeof(TProps).GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                switch (propertyInfo.GetValue(props))
                {
                    case IPropValue propValue:
                        if (!propValue.IsSet)
                            continue;
                        IPropValue propValue1 = (IPropValue) propertyInfo.GetValue(_props);
                        if (propValue1.Equals(propValue))
                            continue;
                        propValue1.TakeValue(propValue);
                        object tmp = _props; // We need to create a new object to update the property value because structs are immutable
                        propertyInfo.SetValue(tmp, propValue1);
                        _props = (TProps) tmp;
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
        
        public void Set<TProps1>(TProps1 props) where TProps1 : struct
        {
            if (typeof(TProps1) != typeof(TProps))
                throw new Exception("Invalid type");
            Set((TProps) (object) props);
        }

        public TProps1 Get<TProps1>() where TProps1 : struct
        {
            if (typeof(TProps1) != typeof(TProps))
                throw new Exception("Invalid type");
            return (TProps1) (object) _props;
        }

        public bool IsDirty 
        {
            get => _isDirty;
            set => _isDirty = value;
        }
    }
}