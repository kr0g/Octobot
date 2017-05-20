using Common.Tasks;

namespace Common
{
    public static class TaskExtensions
    {
        public static ITask Then<T>(this ITask left) where T : ITask, new()
        {
            return new CompositeTask(left, new T());
        }

        public static ITask Then(this ITask left, ITask right)
        {
            return new CompositeTask(left, right);
        }
    }
}