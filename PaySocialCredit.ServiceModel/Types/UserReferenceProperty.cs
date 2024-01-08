using ServiceStack.DataAnnotations;

namespace PaySocialCredit.ServiceModel.Types;

public class UserReferenceProperty
{
    [PrimaryKey] [AutoIncrement] public ulong Id { get; set; }
    [Unique] [StringLength(32)] public string Name { get; set; }
    [Index] [StringLength(512)] public string Value { get; set; }
}