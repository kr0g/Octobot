using System;

namespace Common.Container
{
    public static class IOC
    {
        private static IDependencyResolver IOCInstance;

        public static void Register(IDependencyResolver dependencyResolver)
        {
            IOCInstance = dependencyResolver;
        }

        public static T Resolve<T>()
        {
            AssertContainerInitialized();
            return IOCInstance.GetImplementationOf<T>();
        }
        
        public static void ClearImplementations()
        {
            IOCInstance = null;
        }

        private static void AssertContainerInitialized()
        {
            if (IOCInstance == null)
            {
                throw new InvalidOperationException("IOC not initialized. Call IOC.Register(IDependencyResolver resolver).");
            }
        }
    }
}
