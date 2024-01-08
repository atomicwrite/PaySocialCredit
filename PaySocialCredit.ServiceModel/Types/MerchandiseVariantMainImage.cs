using ServiceStack.DataAnnotations;

namespace PaySocialCredit.ServiceModel.Types;

public class MerchandiseVariantMainImage
{
    [PrimaryKey] [AutoIncrement] public ulong Id { get; set; }

    [Unique]
    [References(typeof(MerchandiseVariant))]
    public ulong MerchandiseVariantId { get; set; }

    [Index] [StringLength(512)] public string URL { get; set; }
}