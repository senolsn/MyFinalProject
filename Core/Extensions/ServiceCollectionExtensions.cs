using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;


namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //Extension için class static olmalı.
        //Bugün Core Module yarın X Module Y Module geleceği için hepsini alt alta tek tek yazmak yerine bir base module yapıp ICoreModule Array gönderip onları tek metotta topladık.
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, ICoreModule[] modules) 
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }
            return ServiceTool.Create(serviceCollection);
        }
    }
}
