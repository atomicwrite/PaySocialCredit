using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace PaySocialCredit.ServiceModel.Types;

public class StoredHttpRequest
{
    [PrimaryKey] [AutoIncrement] public ulong Id { get; set; }
    public StoredHttpRequest(string json,string typeName)
    {
        JSON = json;
        TypeName = typeName;
    }
    public StoredHttpRequest(object request)
    {
        JSON = JsonConvert.SerializeObject(request);
        TypeName = request.GetType().Name;
    }

    [Index] [StringLength(64)] public string TypeName { get; set; }

    [StringLength(StringLengthAttribute.MaxText)]
    public string JSON { get; set; }


    public T ToOriginalType<T>()
    {
        return JsonConvert.DeserializeObject<T>(JSON);
    }
}

public class InProgressRequest :StoredHttpRequest
{
    public InProgressRequest(StoredHttpRequest request) : base(request.JSON,request.TypeName)
    {
    }
}
