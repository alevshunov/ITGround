using System.Threading;
using KudesniK.Extenshions.ReadWriteLocker.Interfaces;

namespace KudesniK.Extenshions.ReadWriteLocker.Lockes
{
    internal class UpgradableReadLock : IUpgradableReadLock
    {
        private readonly ReaderWriterLockSlim _locker;

        public UpgradableReadLock(ReaderWriterLockSlim locker)
        {
            _locker = locker;
            _locker.EnterUpgradeableReadLock();
        }

        public void Dispose()
        {
            _locker.ExitUpgradeableReadLock();
        }

        public IWriteLock UpgradeToWriteLock
        {
            get { return new WriteLock(_locker); }
        }
    }
}