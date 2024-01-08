using ServiceStack.DataAnnotations;

namespace PaySocialCredit.ServiceModel.Types;

public class Category
{
    [PrimaryKey] [AutoIncrement] public ulong Id { get; set; }

    /// <summary>
    /// Parent ID
    /// </summary>
    [References(typeof(Category))]
    public ulong CategoryId { get; set; }

    [Unique] public ulong GoogleCategoryId { get; set; }
    [StringLength(32)] public string Text { get; set; }
}