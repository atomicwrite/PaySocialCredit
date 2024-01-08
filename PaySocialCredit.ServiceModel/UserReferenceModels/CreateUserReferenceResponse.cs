using System;
using ServiceStack;

namespace PaySocialCredit.ServiceModel.CreateUserReferenceModels
{
    public class CreateUserReferenceResponse
    {
        public long Id { get; set; }


        public string Message { get; set; }


        public bool Success { get; set; } = true;


        public ResponseStatus ResponseStatus { get; set; }
    }
}