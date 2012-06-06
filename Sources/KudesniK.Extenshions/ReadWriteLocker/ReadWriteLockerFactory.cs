using KudesniK.Extenshions.ReadWriteLocker.Interfaces;
using KudesniK.Extenshions.ReadWriteLocker.Lockers;

namespace KudesniK.Extenshions.ReadWriteLocker
{
    public class ReadWriteLockerFactory : IReadWriteLockerFactory
    {
        public ILocker CreateLocker()
        {
            return new Locker();
        }
    }
}