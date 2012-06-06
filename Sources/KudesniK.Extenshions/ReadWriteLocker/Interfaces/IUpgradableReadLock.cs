namespace KudesniK.Extenshions.ReadWriteLocker.Interfaces
{
    public interface IUpgradableReadLock : ILock
    {
        IWriteLock UpgradeToWriteLock { get; }
    }
}