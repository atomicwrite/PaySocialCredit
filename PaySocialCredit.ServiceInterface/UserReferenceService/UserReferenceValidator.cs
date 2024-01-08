using PaySocialCredit.ServiceModel.Types;
using ServiceStack.FluentValidation;

namespace PaySocialCredit.ServiceModel.CreateUserReferenceService;

public class UserReferenceValidator : AbstractValidator<UserReference>
{
    public UserReferenceValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage("'Name' should not be empty.");
        RuleFor(r => r.Age).GreaterThan(18).WithMessage("'Age' Should be greater than 18.");
    }
}