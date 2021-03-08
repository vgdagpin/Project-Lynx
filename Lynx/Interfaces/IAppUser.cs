using System;

namespace Lynx.Interfaces
{
    public interface IAppUser
    {
        Guid UserID { get; }
        public Guid SessionUID { get; }

        public T GetUserDetail<T>() where T : class;
    }
}
