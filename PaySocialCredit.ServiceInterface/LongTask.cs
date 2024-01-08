using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using ServiceStack;
using Serilog;
using Serilog.Core;

namespace PaySocialCredit.ServiceInterface;

public class LongTask<T> : LongTaskBase
{
    public LongTask(CancellationTokenSource cts) : base(cts)
    {
    }

    private readonly ConcurrentQueue<LongTaskQueueItem<T>> _queue = new();

    public override int Count()
    {
        return _queue.Count;
    }

    protected override async Task _iteration()
    {
        var value = await _take();
        if (value == null)
        {
            await _sleep(DefaultSleep);
            return;
        }

        try
        {
            await _action(value.Item);

            _finished++;
        }
        catch (Exception e)
        {
            HostContext.Resolve<Logger>()
                .Error("Error in _iteration {Message} Stack: {Stack}", e.Message, e.StackTrace);
        }
    }

    protected virtual async Task _action(T value)
    {
    }

    protected virtual LongTask<T>? GetTop()
    {
        return null;
    }

    private async Task<LongTaskQueueItem<T>?> _take()
    {
        if (_queue.TryDequeue(out var tmp))
            return tmp;
        var man = GetTop();

        if (man == null) return default;
        if (man._queue.Count < 2) return default;
        if (man._queue.TryDequeue(out var tmp2))
            return tmp2;

        return default;
    }

    public async Task Put(LongTaskQueueItem<T> work)
    {
        Task.Run(() => { _queue.Enqueue(work); });
    }

    private int _finished;
}