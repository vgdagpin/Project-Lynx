using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lynx.MobileApp.ViewModels;

namespace Lynx.MobileApp.Common.Interfaces
{
    public interface INavigationService
    {
        LynxViewModel PreviousPageViewModel { get; }
        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>() where TViewModel : LynxViewModel;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : LynxViewModel;
        Task RemoveLastFromBackStackAsync();
        Task RemoveBackStackAsync();
    }
}
