using System.Reflection;

namespace Kekser.ComponentSystem.ComponentBase.PropSystem
{
    public static class PropUtils<TProp> where TProp : class, new()
    {
        private static readonly PropertyInfo[] PropProperties = typeof(TProp).GetProperties();
        
        public static TProp Copy(TProp props)
        {
            TProp result = new TProp();
            foreach (PropertyInfo propertyInfo in PropProperties)
            {
                if (!propertyInfo.CanRead || !propertyInfo.CanWrite)
                    continue;

                switch (propertyInfo.GetValue(props))
                {
                    case IPropValue propValue:
                        if (!propValue.IsSet)
                            continue;
                        propertyInfo.SetValue(result, propValue);
                        break;
                    default:
                        propertyInfo.SetValue(result, propertyInfo.GetValue(props));
                        break;
                }
            }
            return result;
        }
        
        public static TProp Merge(TProp props, TProp newProps)
        {
            TProp result = Copy(props);
            MergeInPlace(result, newProps);
            return result;
        }
        
        public static void MergeInPlace(TProp props, TProp newProps)
        {
            foreach (PropertyInfo propertyInfo in PropProperties)
            {
                if (!propertyInfo.CanRead || !propertyInfo.CanWrite)
                    continue;

                switch (propertyInfo.GetValue(newProps))
                {
                    case IPropValue propValue:
                        if (!propValue.IsSet)
                            continue;
                        IPropValue propValue1 = (IPropValue) propertyInfo.GetValue(props);
                        if (propValue1.Equals(propValue))
                            continue;
                        propValue1.TakeValue(propValue);
                        propertyInfo.SetValue(props, propValue);
                        break;
                    default:
                        if (propertyInfo.GetValue(props) == propertyInfo.GetValue(newProps))
                            continue;
                        propertyInfo.SetValue(props, propertyInfo.GetValue(newProps));
                        break;
                }
            }
        }
    }
    
    public class PropUtils
    {
        public static TProps Copy<TProps>(TProps props) where TProps : class, new()
        {
            return PropUtils<TProps>.Copy(props);
        }
        
        public static TProps Merge<TProps>(TProps props, TProps newProps) where TProps : class, new()
        {
            return PropUtils<TProps>.Merge(props, newProps);
        }
        
        public static void MergeInPlace<TProps>(TProps props, TProps newProps) where TProps : class, new()
        {
            PropUtils<TProps>.MergeInPlace(props, newProps);
        }
    }
}