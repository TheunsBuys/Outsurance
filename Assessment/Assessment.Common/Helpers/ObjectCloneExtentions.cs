using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace Assessment
{
    public static class ObjectCloneExtentions
    {
        public static void ClonePropertiesTo(this object fromObj, object toObj, string[] excludeProperties = null)
        {
            if (fromObj == null) return;
            if (toObj == null)
                toObj = Activator.CreateInstance(fromObj.GetType());
            DoCloneProperties(fromObj, toObj, excludeProperties, null);
        }

        public static void ClonePropertiesTo<TFilterType>(this object fromObj, object toObj, string[] excludeProperties = null)
            where TFilterType : class
        {
            if (fromObj == null) return;
            if (toObj == null)
                toObj = Activator.CreateInstance(fromObj.GetType());
            DoCloneProperties(fromObj, toObj, excludeProperties, typeof(TFilterType));
        }

        public static T CloneProperties<T>(this object fromObj, string[] excludeProperties = null)
            where T : new()
        {
            var item = new T();
            DoCloneProperties(fromObj, item, excludeProperties, null);
            return item;
        }

        public static T CloneProperties<T>(this object fromObj, object toObj, string[] excludeProperties = null)
            where T : new()
        {
            var item = new T();
            if (toObj != null)
            {
                if (item.GetType() == toObj.GetType())
                    item = (T)toObj;
                else
                    DoCloneProperties(toObj, item, null, null);
            }
            DoCloneProperties(fromObj, item, excludeProperties, fromObj.GetType());
            return item;
        }

        public static T CloneProperties<T, TFilterType>(this object fromObj, params string[] excludeProperties)
            where T : new()
            where TFilterType : class
        {
            var item = new T();
            DoCloneProperties(fromObj, item, excludeProperties, typeof(TFilterType));
            return item;
        }

        public static T CloneProperties<T, TFilterType>(this object fromObj, object toObj, string[] excludeProperties = null)
            where T : new()
            where TFilterType : class
        {
            var item = new T();
            if (toObj != null)
            {
                if (item.GetType() == toObj.GetType())
                    item = (T)toObj;
                else
                    DoCloneProperties(toObj, item, null, null);
            }
            DoCloneProperties(fromObj, item, excludeProperties, typeof(TFilterType));
            return item;
        }

        private static void DoCloneProperties(object fromObj, object toObj, string[] excludeProperties, Type filterType)
        {
            if (fromObj == null)
                return;

            if (fromObj is IEnumerable &&
                fromObj.GetType().GetTypeInfo().IsGenericType &&
                toObj is IList &&
                toObj.GetType().GetTypeInfo().IsGenericType)
            {
                var itemType = toObj.GetType().GetTypeInfo().GenericTypeArguments[0];
                foreach (var item in (fromObj as IEnumerable))
                {
                    var tmp = Activator.CreateInstance(itemType);
                    DoCloneProperties(item, tmp, excludeProperties, filterType);
                    ((IList)toObj).Add(tmp);
                }
                return;
            }
            var toProperties = toObj.GetType().GetRuntimeProperties().ToList();
            var fromProperties = fromObj.GetType().GetRuntimeProperties().ToList();
            if (filterType != null)
                fromProperties = fromProperties.Where(x => filterType.GetRuntimeProperties().Select(y => y.Name).ToList().Contains(x.Name)).ToList();
            if (excludeProperties != null && excludeProperties.Any(x => !string.IsNullOrEmpty(x)))
                fromProperties = fromProperties.Where(x => excludeProperties.Contains(x.Name) == false).ToList();
            fromProperties = fromProperties.Where(x => toProperties.Select(y => y.Name).ToList().Contains(x.Name)).ToList();

            foreach (var fromProperty in fromProperties)
            {
                var propertyToSet = toProperties.SingleOrDefault(x => x.Name.Equals(fromProperty.Name) &&
                                                                      x.PropertyType == fromProperty.PropertyType);
                if (propertyToSet != null && propertyToSet.CanWrite && propertyToSet.SetMethod.IsPublic)
                {
                    propertyToSet.SetValue(toObj, fromProperty.GetValue(fromObj, null), null);
                }
            }
        }
    }
}
