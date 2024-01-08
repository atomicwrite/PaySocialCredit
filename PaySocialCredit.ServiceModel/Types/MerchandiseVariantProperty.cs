using ServiceStack.DataAnnotations;

namespace PaySocialCredit.ServiceModel.Types;

public class MerchandiseVariantProperty
{
    [PrimaryKey] [AutoIncrement] public ulong Id { get; set; }
    [References(typeof(Merchandise))] public ulong MerchandiseId { get; set; }

    [References(typeof(MerchandiseVariant))]
    public ulong MerchandiseVariantId { get; set; }

    [Unique] [StringLength(32)] public string Name { get; set; }
    [Index] [StringLength(32)] public string Value { get; set; }
}