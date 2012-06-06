namespace KudesniK.Extenshions
{
    public class CircleItem<T>
    {
        public CircleItem()
        {
            HasItem = false;
        }

        private T _item;
        public CircleItem<T> Next;
        public CircleItem<T> Prev;
        public bool HasItem { get; private set; }

        public T Item
        {
            get { return _item; }
            set
            {
                HasItem = true;
                _item = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Item: {0}", _item);
        }
    }
}