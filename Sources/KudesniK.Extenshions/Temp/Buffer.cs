using System;
using System.Collections;
using System.Collections.Generic;
using KudesniK.Extenshions.ReadWriteLocker;
using KudesniK.Extenshions.ReadWriteLocker.Interfaces;

namespace KudesniK.Extenshions.Temp
{
    public class Buffer<T> : IEnumerable<T>
    {
        protected readonly int BufferSize;
        protected readonly T[] Items;
        protected readonly ILocker Locker;
        protected bool IsStartFromZero = true;
        protected int LastIndex;

        public Buffer(int bufferSize)
        {
            Locker = new ReadWriteLockerFactory().CreateLocker();
            BufferSize = bufferSize;
            Items = new T[BufferSize];
            LastIndex = -1;
        }

        protected virtual void IncreaseLastIndex()
        {
            LastIndex++;
            if (LastIndex == BufferSize)
            {
                LastIndex = 0;
                IsStartFromZero = false;
            }
        }

        public virtual void Add(T item)
        {
            using (Locker.WriteLock)
            {
                IncreaseLastIndex();
                Items[LastIndex] = item;
            }
        }

        public virtual Int32 Length
        {
            get
            {
                if (IsStartFromZero)
                    return LastIndex+1;

                return BufferSize;
            }
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            int start = IsStartFromZero ? 0 : (LastIndex == BufferSize-1 ? 0 : LastIndex + 1);
            int end = LastIndex == -1 ? 0 : LastIndex;
            
            return new Enumerator(Locker.ReadLock, Items, start, end);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal sealed class Enumerator : IEnumerator<T>
        {
            private readonly IReadLock _readLock;
            private readonly T[] _items;
            private readonly int _start;
            private readonly int _end;
            private int _current;
            private bool _isStart;

            public Enumerator(IReadLock readLock, T[] items, int start, int end)
            {
                _readLock = readLock;
                _items = items;
                _start = start;
                _end = end;
                _isStart = true;
                Reset();
            }

            public void Dispose()
            {
                _readLock.Dispose();
            }

            public bool MoveNext()
            {
                if (_current == _end)
                    return false;

                if (!_isStart)
                {
                    _current++;
                    if (_current == _items.Length)
                        _current = 0;
                }
                else
                {
                    _isStart = false;
                }

                return true;
            }

            public void Reset()
            {
                _isStart = true;
                _current = _start;
            }

            public T Current
            {
                get { return _items[_current]; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }
        }
    }
}