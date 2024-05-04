using System;
using System.Reflection;

namespace Kekser.ComponentSystem.ComponentBase.PropSystem.Rework
{
    public class PropList<TProps>: IPropList where TProps: struct
    {
        private TProps _props;
        private bool _isDirty = true;
        
        public TProps Get()
        {
            return _props;
        }
        
        public void Set(Func<TProps, TProps> setter)
        {
            TProps props = setter(_props);
            Set(props);
        }
        
        public void Set(TProps props)
        {
            PropertyInfo[] propertyInfos = typeof(TProps).GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                switch (propertyInfo.GetValue(props))
                {
                    case IPropValue propValue:
                        if (propValue.IsOptional && !propValue.IsSet)
                            continue;
                        if (!propValue.IsOptional && !propValue.IsSet)
                            throw new Exception("Required prop not set");
                        IPropValue propValue1 = (IPropValue) propertyInfo.GetValue(_props);
                        if (propValue1.RawValue == propValue.RawValue)
                            continue;
                        propertyInfo.SetValue(_props, propValue);
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

        public void Set<TProps1>(Func<TProps1, TProps1> setter) where TProps1 : struct
        {
            if (typeof(TProps1) != typeof(TProps))
                throw new Exception("Invalid type");
            Set(setter(Get<TProps1>()));
        }

        public TProps1 Get<TProps1>() where TProps1 : struct
        {
            if (typeof(TProps1) != typeof(TProps))
                throw new Exception("Invalid type");
            return (TProps1) (object) _props;
        }

        public void Set<TProps1>(TProps1 props) where TProps1 : struct
        {
            if (typeof(TProps1) != typeof(TProps))
                throw new Exception("Invalid type");
            Set((TProps) (object) props);
        }

        public bool IsDirty 
        {
            get => _isDirty;
            set => _isDirty = value;
        }
    }
}