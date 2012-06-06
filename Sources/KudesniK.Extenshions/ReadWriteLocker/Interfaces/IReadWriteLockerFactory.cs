namespace KudesniK.Extenshions.ReadWriteLocker.Interfaces
{
    interface IReadWriteLockerFactory
    {
        ILocker CreateLocker();
    }
}