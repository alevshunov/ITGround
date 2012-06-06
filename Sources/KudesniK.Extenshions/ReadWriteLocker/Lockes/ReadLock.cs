using System.Threading;
using KudesniK.Extenshions.ReadWriteLocker.Interfaces;

namespace KudesniK.Extenshions.ReadWriteLocker.Lockes
{
    internal class ReadLock : IReadLock
    {
        private readonly ReaderWriterLockSlim _locker;

        public ReadLock(ReaderWriterLockSlim locker)
        {
            _locker = locker;
            _locker.EnterReadLock();
        }

        public void Dispose()
        {
            _locker.ExitReadLock();
        }
    }
}
