using System;
using ServiceStack;
using PaySocialCredit.ServiceModel;

namespace PaySocialCredit.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
             
            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }
    }
}
