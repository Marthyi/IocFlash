namespace IocFlash.Tests
{
    using System;
    using System.Diagnostics;

    using Autofac;

    using IocFlash;
    using NUnit.Framework;

    [TestFixture]
    public class TestPerformance
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
        public void SimpleInstanciation()
        {
            // Autofac has been selected because it's a really good ioc framework and it's one of the best in performance benchmark.
            var autofacBuilder = new ContainerBuilder();
            autofacBuilder.RegisterType<SampleClass>().As<ISampleClass>();
            IContainer autofac = autofacBuilder.Build();

            IIocFlashContainer container = IocFlashContainer.CreateContainer();
            container.Bind<ISampleClass>().ToSingleton<SampleClass>();

            const int OneMillion = 1000*1000;

            Stopwatch swAutofac = Stopwatch.StartNew();
            for (int i = 0; i < OneMillion; i++)
            {
                var instance = autofac.Resolve<ISampleClass>();
            }
            swAutofac.Stop();

            Stopwatch swIocFlash = Stopwatch.StartNew();
            for (int i = 0; i < OneMillion; i++)
            {
                var instance = autofac.Resolve<ISampleClass>();
            }
            swIocFlash.Stop();
            
            Console.WriteLine(string.Format("Autofac:   {0:### ### ### ###}", swAutofac.ElapsedTicks));
            Console.WriteLine(string.Format("IocFlash : {0:### ### ### ###}", swIocFlash.ElapsedTicks));
            Assert.That(swIocFlash.ElapsedTicks,Is.LessThan(swAutofac.ElapsedTicks));
        }
    }
}
