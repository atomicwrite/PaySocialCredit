using System.Collections.Generic;
using PaySocialCredit.ServiceModel.Types;
using ServiceStack.Data;
using ServiceStack.OrmLite;


public class ResponseProxy
{
    private IDbConnectionFactory _dbConnectionFactory;

    public ResponseProxy(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public void StoreHttpRequest<T>(T request)
    {
        using var dbConnection = _dbConnectionFactory.Open();
        dbConnection.Insert(new StoredHttpRequest(request));
    }

    public T MarkAndGetNextRequest<T>()
    {
        var nextRequest = GetNextRequestType();
        if (nextRequest == null)
        {
            // todo: warn that queue is empty, maybe increase sleep
            return default;
        }
        
        using var dbConnection = _dbConnectionFactory.Open(); //opens transaction
        using var transaction = dbConnection.BeginTransaction();
        dbConnection.Insert(new InProgressRequest(nextRequest));
        dbConnection.Delete(nextRequest);
        transaction.Commit();
        return nextRequest.ToOriginalType<T>();
    }


    private StoredHttpRequest GetNextRequestType()
    {
        using var dbConnection = _dbConnectionFactory.Open(); //opens transaction
        return dbConnection.Single<StoredHttpRequest>(a => true);
    }
}