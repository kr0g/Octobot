namespace Common.Container
{
    public interface IDependencyResolver
    {
        T GetImplementationOf<T>();
    }
}
