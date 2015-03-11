namespace IocFlash
{
    using System;
    using System.Collections.Generic;

    public static class ToSingletonInstanceIIocFlashBindableExtension
    {
        private static readonly Dictionary<Type, Lazy<object>> SingletonDictionary = new Dictionary<Type, Lazy<object>>(); 
        
        public static void ToSingleton<TClass>(this IIocFlashBindable bindableContainer)
        {            
            ToSingleton(bindableContainer, typeof(TClass));
        }

        public static void ToSingleton(this IIocFlashBindable bindableContainer, Type targetType)
        {
         
            var ctorFunc = ResolutionBuilder.BuildConstructorFunction(bindableContainer.Container, targetType);
            SingletonDictionary.Add(bindableContainer.BindingType, new Lazy<object>(ctorFunc));

            bindableContainer.AddResolution(() => SingletonDictionary[bindableContainer.BindingType].Value);
        }
    }
}