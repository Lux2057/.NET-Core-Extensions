namespace Extensions;

#region << Using >>

#region << Using >>

using System.Linq.Expressions;
using System.Reflection;

#endregion

#endregion

public static class ReflectionExt
{
    /// <summary>
    ///     Returns a PropertyInfo from a member lambda expression
    /// </summary>
    public static PropertyInfo GetPropertyInfo<TSource, TProperty>(this Expression<Func<TSource, TProperty>> propertyLambda)
    {
        if (propertyLambda == null)
            throw new ArgumentNullException(nameof(propertyLambda));

        if (propertyLambda.Body is not MemberExpression member)
            throw new ArgumentException($"Expression '{propertyLambda}' refers to a method, not a property.");

        if (member.Member is not PropertyInfo propInfo)
            throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");

        var type = typeof(TSource);
        if (propInfo.ReflectedType != null && type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            throw new ArgumentException($"Expression '{propertyLambda}' refers to a property that is not from type {type}.");

        return propInfo;
    }

    /// <summary>
    ///     Returns value of a property by member lambda expression
    /// </summary>
    public static TProperty GetPropertyValueOrDefault<TSource, TProperty>(this TSource obj, Expression<Func<TSource, TProperty>> propertyLambda)
    {
        if (obj == null)
            return default;

        return (TProperty)propertyLambda.GetPropertyInfo().GetValue(obj);
    }

    /// <summary>
    ///     Sets value of a property by member lambda expression
    /// </summary>
    public static void SetPropertyValue<TSource, TProperty>(this TSource obj, Expression<Func<TSource, TProperty>> propertyLambda, TProperty value)
    {
        if (obj == null)
            return;

        propertyLambda.GetPropertyInfo().SetValue(obj, value);
    }
}