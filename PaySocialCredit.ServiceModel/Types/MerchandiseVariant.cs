using ServiceStack.DataAnnotations;

namespace PaySocialCredit.ServiceModel.Types;

public class MerchandiseVariant
{
    [PrimaryKey] [AutoIncrement] public ulong Id { get; set; }
    [References(typeof(Merchandise))] public ulong MerchandiseId { get; set; }

    public decimal Cost { get; set; }
}