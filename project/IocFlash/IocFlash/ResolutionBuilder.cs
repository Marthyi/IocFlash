namespace IocFlash
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class ResolutionBuilder
    {
        public static Func<object> BuildConstructorFunction(IIocFlashContainer container, Type resolutionType)
        {
            ConstructorInfo[] ctors = resolutionType.GetConstructors();

            if (ctors.Where(p => p.IsPublic).ToArray().Length > 1)
            {
                throw new InvalidOperationException("Cannot resolve constructor");
            }

            var parameterTypes = ctors[0].GetParameters().Select(p => p.ParameterType).ToArray();

            if (parameterTypes.Length == 0)
            {
                return () => ctors[0].Invoke(null);
            }
            else
            {
                object[] parameters = parameterTypes.Select(container.Get).ToArray();
                return () => ctors[0].Invoke(parameters);
            }
        }
    }
}
