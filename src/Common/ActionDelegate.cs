using System.Threading;

namespace Common
{
    public delegate void ActionDelegate();
    public delegate void ActionDelegate<in T>(T arg1);
    public delegate void ActionDelegate<in T, in T2>(T arg1, T2 arg2);
    public delegate T FunctionDelegate<T>();
    public delegate ReturnType FunctionDelegate<out ReturnType, in Argument1Type>(Argument1Type arg1);
    public delegate ReturnType FunctionDelegate<out ReturnType, in Argument1Type, in Argument2Type>(Argument1Type arg1, Argument2Type arg2);

    public static class AsynchronousAction
    {
        public static void Invoke(ActionDelegate action)
        {
            Invoke(action, true);
        }

        public static Thread Invoke(ActionDelegate action, bool useDelegation)
        {
            if (useDelegation)
            {
                action.BeginInvoke(null, null);
                return null;
            }

            var thread = new Thread(() => action());
            thread.Start();
            return thread;
        }


    }
}
