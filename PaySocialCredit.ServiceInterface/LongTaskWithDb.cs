using System;
using System.Data;
using System.Threading;
using ServiceStack;

namespace PaySocialCredit.ServiceInterface;

public class LongTaskWithDb<T>(CancellationTokenSource cts) : LongTask<T>(cts), IDisposable
{
    protected readonly IDbConnection _db = HostContext.Resolve<IDbConnection>();
    public void Dispose() => _db?.Dispose();
};