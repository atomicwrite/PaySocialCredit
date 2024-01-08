using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ServiceStack;

namespace PaySocialCredit.ServiceModel.CreateUserReferenceService;

public class EnabledServices
{
    private ConcurrentDictionary<Type, bool> _allowed;

    public bool Allowed(Type type)
    {
        return _allowed.ContainsKey(type);
    }

    public void Add(Type type)
    {
        _allowed.AddIfNotExists(new KeyValuePair<Type, bool>(type, true));
    }
}