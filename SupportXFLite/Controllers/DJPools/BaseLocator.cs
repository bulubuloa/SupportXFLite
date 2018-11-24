using System;
using Autofac;

namespace SupportXFLite.Controllers.DJPools
{
    public class BaseLocator
    {
        private IContainer container;
        protected ContainerBuilder PoolBuilder;

        public BaseLocator()
        {
            Initialize();
        }

        private void Initialize()
        {
            PoolBuilder = new ContainerBuilder();
            CreatePoolObject();
        }

        protected virtual void CreatePoolObject()
        {

        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return container.Resolve(type);
        }

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            PoolBuilder.RegisterType<TImplementation>().As<TInterface>();
        }

        public void Register<T>() where T : class
        {
            PoolBuilder.RegisterType<T>();
        }

        public void Build()
        {
            container = PoolBuilder.Build();
        }
    }
}