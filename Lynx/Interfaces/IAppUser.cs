using System;

namespace Lynx.Interfaces
{
    public interface IAppUser
    {
        Guid UserID { get; }
        Guid SessionUID { get; }

        T GetUserDetail<T>() where T : class;
    }
}
