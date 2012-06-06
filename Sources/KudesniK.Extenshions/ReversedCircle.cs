using System;
using System.Collections;
using System.Collections.Generic;
using KudesniK.Extenshions.ReadWriteLocker;
using KudesniK.Extenshions.ReadWriteLocker.Interfaces;

namespace KudesniK.Extenshions
{
    public class ReversedCircle<T> : IEnumerable<T>
    {
        private ILocker _locker = new ReadWriteLockerFactory().CreateLocker();

        public ReversedCircle(int size)
        {
            _root = new CircleItem<T>();

            var current = _root;
            for (var i=0; i<size; i++)
            {
                current.Next = new CircleItem<T>();
                current.Next.Prev = current;
                current = current.Next;
            }

            current.Next = _root.Next;
            _root.Next.Prev = current;
            _root.Prev = null;
            _nextFree = _root.Next;
        }

        private CircleItem<T> _nextFree;
        private readonly CircleItem<T> _root;

        public void Add(T item)
        {
            using (_locker.WriteLock)
            {
                _nextFree.Item = item;
                _nextFree = _nextFree.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(_nextFree, _locker.ReadLock);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected class Enumerator : IEnumerator<T>
        {
            private readonly CircleItem<T> _root;
            private readonly IReadLock _readLock;
            private CircleItem<T> _current;
            private bool _isEnd = false;

            public Enumerator(CircleItem<T> root, IReadLock readLock)
            {
                _root = root;
                _readLock = readLock;
                _current = root;
            }

            public void Dispose()
            {
                _readLock.Dispose();
            }

            public bool MoveNext()
            {
                if (!_current.Prev.HasItem)
                    return false;

                if (_isEnd)
                    return false;

                if (_current.Prev == _root)
                    _isEnd = true;

                _current = _current.Prev;

                return true;
            }

            public void Reset()
            {
                _current = _root;
            }

            public T Current
            {
                get { return _current.Item; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }
        }

        public override string ToString()
        {
            return String.Join(", ", this);
        }
    }
}
