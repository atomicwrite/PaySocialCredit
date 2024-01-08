 
public class LongTaskQueueItem<T>
{
    public LongTaskQueueItem(T item)
    {
        Item = item;
    }

    public T Item { get; protected init; }
}