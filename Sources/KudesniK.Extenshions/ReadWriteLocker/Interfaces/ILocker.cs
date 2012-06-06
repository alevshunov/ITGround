using System;

namespace KudesniK.Extenshions.ReadWriteLocker.Interfaces
{
    public interface ILocker : IDisposable
    {
        IReadLock ReadLock { get; }

        IWriteLock WriteLock { get; }

        IUpgradableReadLock UpgradableReadLock { get; }
    }
}