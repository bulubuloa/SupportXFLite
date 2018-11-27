using System;
namespace SupportXFLite.Controllers.DJPools
{
    public interface IStandardLocator
    {
        T Resolve<T>();
        object Resolve(Type type);
        void Register<TInterface, TImplementation>() where TImplementation : TInterface;
        void Register<T>() where T : class;
        void Build();
    }
}