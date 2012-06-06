using System.Threading;
using KudesniK.Extenshions.ReadWriteLocker.Interfaces;

namespace KudesniK.Extenshions.ReadWriteLocker.Lockes
{
    internal class WriteLock : IWriteLock
    {
        private readonly ReaderWriterLockSlim _locker;

        public WriteLock(ReaderWriterLockSlim locker)
        {
            _locker = locker;
            _locker.EnterWriteLock();
        }

        public void Dispose()
        {
            _locker.ExitWriteLock();
        }
    }
}