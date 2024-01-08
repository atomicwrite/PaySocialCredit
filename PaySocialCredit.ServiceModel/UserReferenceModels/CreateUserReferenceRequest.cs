using System;
using ServiceStack;
using PaySocialCredit.ServiceModel.Types;
using ServiceStack.DataAnnotations;

namespace PaySocialCredit.ServiceModel.CreateUserReferenceModels
{
    public class RouteCreateUserReferenceRequest
    {
        [PrimaryKey] [AutoIncrement] public ulong Id { get; set; }
        public CreateUserReferenceRequest CreateUserReferenceRequest { get; set; }
        public Guid RouteId { get; set; }
    }
    public class CreateUserReferenceRequest : IReturn<CreateUserReferenceResponse>
    {
        public string name;
        public int age;


        public UserReference UserReference { get; set; }
    }
}