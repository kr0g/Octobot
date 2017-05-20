using System;

namespace Common
{
    public static class EventExtensions
    {
        public static void SafeInvoke(this EventHandler handler, object sender, EventArgs e)
        {
            handler?.Invoke(sender, e);
        }

        public static void SafeInvoke<T>(this EventHandler<T> handler, object sender, T e) where T : EventArgs
        {
            handler?.Invoke(sender, e);
        }
    }
}