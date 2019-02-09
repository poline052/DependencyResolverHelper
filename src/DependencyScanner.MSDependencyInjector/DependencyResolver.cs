using Com.DependencyMappingContracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

//#if NETCORE
using Microsoft.Extensions.DependencyModel;
//#endif

namespace Com.DependencyScanner.MSDependencyInjector
{
    public class DependencyResolver
    {
        private readonly List<string> _registerdCompanyNames = new List<string>() { "Com" };


        private void LoadReferencingAssemblies()
        {

//#if NETCORE
            foreach (var library in DependencyContext.Default.CompileLibraries.Where(p => _registerdCompanyNames.Any(p.Name.StartsWith)))
                Assembly.Load(library.Name);
//#endif
        }

        public void Resolve(IServiceCollection serviceCollection)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }
            // serviceCollection.Options.AllowOverridingRegistrations = true;
            var priorityCache = new Dictionary<string, int>();


            LoadReferencingAssemblies();

            var type = typeof(IDependencyMapping);
            var dependencyRegisterList = AppDomain.CurrentDomain
                .GetAssemblies().Where(asm => _registerdCompanyNames.Any(asm.FullName.StartsWith))
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);

            foreach (var dependencyRegister in dependencyRegisterList)
            {

                var dependencyMapping = (IDependencyMapping)Activator.CreateInstance(dependencyRegister);
                var typesToRegister = dependencyMapping.GetTypesToRegister();

                foreach (var typeToRegister in typesToRegister)
                {
                    if (!string.IsNullOrEmpty(typeToRegister.TypeFrom.FullName))
                    {
                        var existingPriority = priorityCache.ContainsKey(typeToRegister.TypeFrom.FullName) ? priorityCache[typeToRegister.TypeFrom.FullName] : 0;
                        if (typeToRegister.Priority > 0)
                        {

                            if (typeToRegister.Priority > existingPriority)
                            {
                                var descriptor = new ServiceDescriptor(typeToRegister.TypeFrom, typeToRegister.TypeTo, GetLifeStyle(typeToRegister.Lifestyle));
                                serviceCollection.Add(descriptor);
                            }

                            if (priorityCache.ContainsKey(typeToRegister.TypeFrom.FullName))
                            {
                                if (priorityCache[typeToRegister.TypeFrom.FullName] < typeToRegister.Priority)
                                    priorityCache[typeToRegister.TypeFrom.FullName] = typeToRegister.Priority;
                            }
                            else
                            {
                                priorityCache.Add(typeToRegister.TypeFrom.FullName, typeToRegister.Priority);
                            }
                        }
                        else
                        {
                            if (existingPriority <= typeToRegister.Priority)
                            {
                                var descriptor = new ServiceDescriptor(typeToRegister.TypeFrom, typeToRegister.TypeTo, GetLifeStyle(typeToRegister.Lifestyle));
                                serviceCollection.Add(descriptor);
                            }
                        }

                    }
                }

            }

            ServiceLifetime GetLifeStyle(ObjectLifeStyle? code)
            {
                switch (code)
                {
                    case ObjectLifeStyle.Scoped:
                        return ServiceLifetime.Scoped;

                    case ObjectLifeStyle.Singleton:
                        return ServiceLifetime.Singleton;

                    case ObjectLifeStyle.Transient:
                        return ServiceLifetime.Transient;

                    default:
                        return ServiceLifetime.Transient;
                }
            }
        }
    }
}
