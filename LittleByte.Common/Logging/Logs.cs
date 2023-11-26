namespace LittleByte.Common.Logging;

public static class Logs
{
    public delegate ILog LogFactoryDelegate(Type forType);

    public static LogFactoryDelegate LogFactory { get; set; } = _ => NullLog.Instance;

    public static ILog NewLogger(this object @this)
    {
        var log = LogFactory(@this.GetType());

        if(@this is ILoggableKeyValue loggable)
        {
            log.Push(loggable);
        }

        return log;
    }
}