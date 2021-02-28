using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lynx.Domain.ViewModels;

namespace Lynx.Interfaces
{
    public interface ISignInManager
    {
        Task<UserSessionVM> SignInAsync(string username, string password);

        Task SignOutAsync();
    }
}
