using ServiceStack.DataAnnotations;

namespace PaySocialCredit.ServiceModel.Types;

public class AllowedMerchandiseVariantProperty
{
    [PrimaryKey] [AutoIncrement] public ulong Id { get; set; }
    [Unique] [StringLength(32)] public string Name { get; set; }
    [Index] [StringLength(32)] public string Value { get; set; }
}