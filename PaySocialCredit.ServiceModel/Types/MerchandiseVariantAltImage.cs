using ServiceStack.DataAnnotations;

namespace PaySocialCredit.ServiceModel.Types;

public class MerchandiseVariantAltImage
{
    [PrimaryKey] [AutoIncrement] public ulong Id { get; set; }

    [Index]
    [References(typeof(MerchandiseVariant))]
    public ulong MerchandiseVariantId { get; set; }

    [Index] [StringLength(512)] public string URL { get; set; }
}