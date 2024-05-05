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
            foreach (PropertyInfo propertyInfo in PropProperties)
            {
                switch (propertyInfo.GetValue(newProps))
                {
                    case IPropValue propValue:
                        if (!propValue.IsSet)
                            continue;
                        propertyInfo.SetValue(result, propValue);
                        break;
                    default:
                        propertyInfo.SetValue(result, propertyInfo.GetValue(newProps));
                        break;
                }
            }
            return result;
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
    }
}