namespace IocFlash.Tests
{
    using IocFlash;
    using NUnit.Framework;

    [TestFixture]
    public class TestIocFlashContainer
    {
        interface ISampleClass
        {
            void DoSomething();
        }

        public class SampleClass : ISampleClass
        {
            public void DoSomething()
            {
                throw new System.NotImplementedException();
            }
        }

        [Test]
        public void BindInterfaceToClass()
        {
            IIocFlashContainer container = IocFlashContainer.CreateContainer();
            container.Bind<ISampleClass>().To<SampleClass>();

            var instance = container.Get<ISampleClass>();
            Assert.IsNotNull(instance);

            var instance2 = container.Get<ISampleClass>();
            Assert.IsNotNull(instance);
            Assert.AreNotSame(instance, instance2);
        }

        [Test]
        public void BindInterfaceToSingleton()
        {
            IIocFlashContainer container = IocFlashContainer.CreateContainer();
            container.Bind<ISampleClass>().ToSingleton<SampleClass>();

            var instance1 = container.Get<ISampleClass>();
            var instance2 = container.Get<ISampleClass>();

            Assert.IsNotNull(instance1);
            Assert.AreSame(instance1, instance2);
        }
    }
}
