namespace Common.Commands
{
    public static class CommandExtensions
    {
        public static ICommand<T> Then<T>(this ICommand<T> left, ICommand<T> right)
        {
            return new CompositeCommand<T>(left, right);
        }
    }
}
