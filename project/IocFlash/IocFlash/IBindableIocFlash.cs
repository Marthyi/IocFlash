namespace IocFlash
{
    using System;

    public interface IIocFlashBindable
    {
        void AddResolution(Func<object> func);

        IIocFlashContainer Container { get; }

        Type BindingType { get; }
    }
}
