namespace IocFlash
{
    using System;

    public static class ToNewInstanceIIocFlashBindableExtension
    {
        public static void To<TClass>(this IIocFlashBindable bindableContainer)
        {
            To(bindableContainer, typeof(TClass));
        }

        public static void To(this IIocFlashBindable bindableContainer,Type classType)
        {
            Func<object> ctorFunc = ResolutionBuilder.BuildConstructorFunction(bindableContainer.Container, classType);
            bindableContainer.AddResolution(ctorFunc);
        }       
    }
}