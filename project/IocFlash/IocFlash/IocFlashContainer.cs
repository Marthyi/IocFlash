namespace IocFlash
{
    using System;
    using System.Collections.Generic;

    public class IocFlashContainer : IIocFlashBindable, IIocFlashContainer
    {
        private readonly Dictionary<Type, Func<object>> containerDictionary = new Dictionary<Type, Func<object>>();

        public Type BindingType { get; private set; }

        private IocFlashContainer()
        {
        }

        public static IIocFlashContainer CreateContainer()
        {
            return new IocFlashContainer();
        }

        public T Get<T>() where T : class
        {
            return Get(typeof(T)) as T;
        }

        public object Get(Type t)
        {
            return this.containerDictionary[t].Invoke();
        }

        public void AddResolution(Func<object> func)
        {
            containerDictionary.Add(BindingType, func);
        }

        public IIocFlashContainer Container
        {
            get
            {
                return this;
            }
        }

        public IIocFlashBindable Bind<TClass>()
        {
            return this.Bind(typeof(TClass));
        }

        public IIocFlashBindable Bind(Type classType)
        {
            this.BindingType = classType;
            return this;
        }
    }
}
