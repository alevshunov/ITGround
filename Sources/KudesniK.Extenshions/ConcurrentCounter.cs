namespace KudesniK.Extenshions
{
    public class ConcurrentCounter
    {
        private int _counter;

        public ConcurrentCounter() : this(0)
        {
        }

        public ConcurrentCounter(int startValue)
        {
            _counter = startValue;
        }

        public int GetNext()
        {
            lock (this)
            {
                _counter++;
                return _counter;
            }
        }
    }
}