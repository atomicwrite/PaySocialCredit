using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace PaySocialCredit.ServiceModel.CreateUserReferenceService;

 

public class PayException : Exception
{
    public PayException(string noRoutesTo)
    {
        throw new NotImplementedException();
    }
}

public class PaySocialNode
{
    public HttpClient IpAddress { get; set; }
    public int CurrentLoad { get; set; }
}

public class NodeConfigSettings(bool IsDbServer, string RouteServerIpAddress, string IpAddress )
{
    [PrimaryKey] [AutoIncrement] public ulong Id { get; set; }
    public bool IsDbServer { get; } = IsDbServer;
    private string RouteServerIpAddress { get; } = RouteServerIpAddress;
    private JsonApiClient _routeServerClient = null;

    public JsonApiClient RouteServerClient()
    {
        if (_routeServerClient == null)
        {
            return _routeServerClient = new JsonApiClient(RouteServerIpAddress);
        }

        return _routeServerClient;
    }
    private Dictionary<Type, PaySocialNode[]> _routes;

    public JsonApiClient GetNodeFor<T>()
    {
        var type = typeof(T);
        if (!_routes.TryGetValue(type, out var ServiceNodes) || ServiceNodes.Length == 0) 
            throw new PayException($"No routes to {type.Name}");

        var lowestStress = ServiceNodes[ServiceNodes.Min(a => a.CurrentLoad)];
        return new JsonApiClient(lowestStress.IpAddress);
    }
    public string IpAddress { get; set; } = IpAddress;

    public bool IsRoutingServer()
    {
        throw new NotImplementedException();
    }
}