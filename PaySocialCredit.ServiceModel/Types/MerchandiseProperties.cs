using ServiceStack.DataAnnotations;

namespace PaySocialCredit.ServiceModel.Types;

public class MerchandiseProperties
{
    [PrimaryKey] [AutoIncrement] public ulong Id { get; set; }

    [References(typeof(Merchandise))] public ulong MerchandiseId { get; set; }

    [StringLength(StringLengthAttribute.MaxText)]
    public string Text { get; set; }
}