using Com.DependencyMappingContracts;

namespace Com.SampleLibrary
{
    public class DependencyMappings : DependencyMapping
    {
        public DependencyMappings()
        {
            Add<IFibonacciGenerator, FibonacciGeneratorUsingDP>();
        }
    }
}
