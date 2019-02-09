
using Microsoft.Extensions.DependencyInjection;

namespace Com.DependencyScanner.MSDependencyInjector
{
   public static class DependencyResolverExtensions
    {
        public static void ScanDependency(this IServiceCollection serviceCollection)
        {
            new DependencyResolver().Resolve(serviceCollection);
        }
    }
}
