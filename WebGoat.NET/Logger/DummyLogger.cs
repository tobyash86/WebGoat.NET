using System;

namespace WebGoat.NET.Logger
{
    public static partial class DummyLogger
    {
        public static void Log(object message)
        {
            LogString(message as string);
        }
    }
}
