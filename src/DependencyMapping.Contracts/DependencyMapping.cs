using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.DependencyMappingContracts
{
    public abstract class DependencyMapping : IDependencyMapping
    {
        private readonly IList<TypeMap> _dependencyMaps = new List<TypeMap>();

        public void Add<TContract, TConcreate>()
        {
            _dependencyMaps.Add(new TypeMap(typeof(TContract), typeof(TConcreate)));
        }

        public void Add<TContract, TConcreate>(ObjectLifeStyle lifeStyle)
        {
            _dependencyMaps.Add(new TypeMap(typeof(TContract), typeof(TConcreate), lifeStyle));
        }

        public void Add<TContract, TConcreate>(ObjectLifeStyle lifeStyle, int priority)
        {
            _dependencyMaps.Add(new TypeMap(typeof(TContract), typeof(TConcreate), lifeStyle, priority));
        }

        public void Add<TContract, TConcreate>(int priority)
        {
            _dependencyMaps.Add(new TypeMap(typeof(TContract), typeof(TConcreate), priority));
        }

        public void Add<TContract, TConcreate>(TContract from, TConcreate to, ObjectLifeStyle lifeStyle, int priority)
        {
            _dependencyMaps.Add(new TypeMap(from.GetType(), to.GetType(), lifeStyle, priority));
        }

        public IList<TypeMap> GetTypesToRegister()
        {
            return _dependencyMaps;
        }
    }
}
