using ServiceStack.DataAnnotations;

namespace PaySocialCredit.ServiceModel.Types;

[CompositeKey("Id", "Id2")]
public class UserReference
{
    //use second id as shard, evening out for noisy ids
    [PrimaryKey] [AutoIncrement] public ulong Id { get; set; }
    [Index] public ulong Id2 { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

// add a value to here when you hit the end of Id values, then reset autoincrement to 0
// always use the last value in here + 1 as the block unless the number of values is 0, then use 0
public class UserReferenceCompletedBlock
{
    [PrimaryKey] [AutoIncrement] public ulong Id { get; set; }
    [Index] public ulong Id2 { get; set; }
}