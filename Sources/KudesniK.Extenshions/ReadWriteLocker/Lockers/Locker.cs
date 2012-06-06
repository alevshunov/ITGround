using System.Threading;
using KudesniK.Extenshions.ReadWriteLocker.Interfaces;
using KudesniK.Extenshions.ReadWriteLocker.Lockes;

namespace KudesniK.Extenshions.ReadWriteLocker.Lockers
{
    internal class Locker : ILocker
    {
        private readonly ReaderWriterLockSlim _locker = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        public IReadLock ReadLock
        {
            get { return new ReadLock(_locker); }
        }

        public IWriteLock WriteLock
        {
            get { return new WriteLock(_locker); }
        }

        public IUpgradableReadLock UpgradableReadLock
        {
            get { return new UpgradableReadLock(_locker); }
        }

        public void Dispose()
        {
            _locker.Dispose();
        }
    }
}