using Com.DependencyMappingContracts;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#if NETCORE
using Microsoft.Extensions.DependencyModel;
#endif

namespace Com.DependencyScanner.SimpleInjector
{
    public class DependencyResolver
    {
        private readonly List<string> _registerdCompanyNames = new List<string>() { "Selise" };


        private void LoadReferencingAssemblies()
        {

#if NETCORE
            foreach (var library in DependencyContext.Default.CompileLibraries.Where(p => _registerdCompanyNames.Any(p.Name.StartsWith)))
                Assembly.Load(library.Name);
#endif
        }

        public void Resolve(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }
            container.Options.AllowOverridingRegistrations = true;
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
                                container.Register(typeToRegister.TypeFrom, typeToRegister.TypeTo,
                                    GetLifeStyle(typeToRegister.Lifestyle, container));


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
                                container.Register(typeToRegister.TypeFrom, typeToRegister.TypeTo, GetLifeStyle(typeToRegister.Lifestyle, container));
                        }
                    }

                }
            }

        }

        private Lifestyle GetLifeStyle(ObjectLifeStyle? code, Container container)
        {
            switch (code)
            {
                case ObjectLifeStyle.Scoped:
                    return Lifestyle.Scoped;

                case ObjectLifeStyle.Singleton:
                    return Lifestyle.Singleton;

                case ObjectLifeStyle.Transient:
                    return Lifestyle.Transient;

                default:
                    return container.Options.DefaultLifestyle ?? Lifestyle.Transient;
            }
        }
    }
}
