namespace Lynx.Interfaces
{
    public interface IDataSecure
    {
        string Protect<T>(T input, bool sessionBase = true);
        T Unprotect<T>(string input, bool sessionBase = true);
    }
}
