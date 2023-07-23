﻿namespace Known.Razor;

public sealed class CallbackHelper
{
    private static readonly ConcurrentDictionary<string, Dictionary<string, Delegate>> actions = new();

    private CallbackHelper() { }

    public static void Register(string id, string key, Delegate action)
    {
        var list = actions.GetOrAdd(id, k => new Dictionary<string, Delegate>());
        if (list.TryGetValue(key, out Delegate old))
            list[key] = Delegate.Combine(old, action);
        else
            list.Add(key, action);
    }

    public static void Dispose(string id)
    {
        if (actions.Remove(id, out Dictionary<string, Delegate> handlers))
        {
            handlers.Clear();
        }
    }

    [JSInvokable]
    public static Task<object> CallbackAsync(string id, string key)
    {
        if (actions.TryGetValue(id, out Dictionary<string, Delegate> handlers))
        {
            if (handlers.TryGetValue(key, out Delegate d))
            {
                var data = d.DynamicInvoke();
                return Task.FromResult(data);
            }
        }

        return null;
    }

    [JSInvokable]
    public static Task<object> CallbackByParamAsync(string id, string key, Dictionary<string, object> args)
    {
        if (actions.TryGetValue(id, out Dictionary<string, Delegate> handlers))
        {
            if (handlers.TryGetValue(key, out Delegate d))
            {
                var data = d.DynamicInvoke(args);
                return Task.FromResult(data);
            }
        }

        return null;
    }
}