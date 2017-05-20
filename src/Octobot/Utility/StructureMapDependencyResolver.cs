using Common.Container;
using StructureMap;

namespace Octobot.Utility
{
    public class StructureMapDependencyResolver : IDependencyResolver
        {
            private readonly Container container;

            public StructureMapDependencyResolver(Container container)
            {
                this.container = container;
            }

            public T GetImplementationOf<T>()
            {
                return container.GetInstance<T>();
            }
        }
    }
