namespace Common.Commands
{
    public interface ICommand
    {
        void Run();
    }

    public interface ICommand<T>
    {
        T Run(T item);
    }
}
