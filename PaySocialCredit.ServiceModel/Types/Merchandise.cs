using ServiceStack.DataAnnotations;

namespace PaySocialCredit.ServiceModel.Types;

[CompositeIndex("UserReferenceId", "CategoryId")]
public class Merchandise
{
    [PrimaryKey] [AutoIncrement] public ulong Id { get; set; }
    [Index] [References(typeof(Category))] public ulong CategoryId { get; set; }


    [References(typeof(UserReference))] public ulong UserReferenceId { get; set; }
}