using System;
using System.Threading;
using System.Threading.Tasks;

public partial class LongTaskWithReturn<T, TR>
{
    int _finished = 0;



    public class ReturnQueue
    {
        private CancellationTokenSource _source = new CancellationTokenSource();
        private TR? _value;

        public void SetValue(TR? value)
        {

            _value = value;
            _source.Cancel();
        }

        public TR? GetReturn()
        {
            try
            {
                Task.Delay(-1, _source.Token).Wait();
            }
            catch (Exception e)
            {
            }



            return _value;
        }
    }
}