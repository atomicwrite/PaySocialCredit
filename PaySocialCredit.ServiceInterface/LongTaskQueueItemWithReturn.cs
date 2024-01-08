 
namespace PaySocialCredit.ServiceInterface;

public class LongTaskQueueItemWithReturn<T, TR> : LongTaskQueueItem<T>
{
    public LongTaskQueueItemWithReturn(T item, LongTaskWithReturn<T, TR>.ReturnQueue returnQueue) : base(item)
    {
        Item = item;
        ReturnQueue = returnQueue;
    }


    public LongTaskWithReturn<T, TR>.ReturnQueue ReturnQueue { get; }
}