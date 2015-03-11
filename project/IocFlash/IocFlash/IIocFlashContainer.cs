namespace IocFlash
{
    using System;

    public interface IIocFlashContainer
    {
        IIocFlashBindable Bind<TClass>();

        IIocFlashBindable Bind(Type classType);

        TClass Get<TClass>() where TClass : class;

        object Get(Type classType);
    }
}