using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lynx.MobileApp.ViewModels;

namespace Lynx.MobileApp.Common.Interfaces
{
    public interface INavigationService
    {
        BaseViewModel PreviousPageViewModel { get; }
        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;
        Task RemoveLastFromBackStackAsync();
        Task RemoveBackStackAsync();
    }
}
