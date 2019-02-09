using System.Collections.Generic;

namespace Com.DependencyMappingContracts
{
    public interface IDependencyMapping
    {
        IList<TypeMap> GetTypesToRegister();
    }
}
