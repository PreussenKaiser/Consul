namespace Consul.Extensions;

public static class TypeExtensions
{
    public static bool IsImplementOf<TInterface>(this Type type)
        where TInterface : class
    {
        return type.IsImplementOf(typeof(TInterface));
    }

    public static bool IsImplementOf(this Type implementingType, Type interfacType)
    {
        IEnumerable<Type> interfaces = implementingType.GetInterfaces();

        bool result = interfaces.Contains(interfacType);

        return result;
    }
}
