using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Lynx.Interfaces;
using Lynx.MobileApp.Models;
using Lynx.MobileApp.Services;
using Microsoft.Extensions.Logging;
using TasqR;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private ITasqR p_TasqR;
        protected ITasqR TasqR
        {
            get
            {
                if (p_TasqR == null)
                {
                    p_TasqR = GetService<ITasqR>();
                }

                return p_TasqR;
            }
        }

        private IAppUser p_appUser;
        protected IAppUser AppUser
        {
            get
            {
                if (p_appUser == null)
                {
                    p_appUser = GetService<IAppUser>();
                }

                return p_appUser;
            }
        }

        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        public virtual bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public virtual string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,            
            Action onChanged = null,
            [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected T GetService<T>()
        {
            return App.ServiceProvider.GetService<T>();
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        protected void LogError(Exception ex)
        {
            ILogger p_ExceptionHandler = GetService<ILogger>();

            if (p_ExceptionHandler != null)
            {
                p_ExceptionHandler.LogError(ex);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(ex.InnermostException().Message);
            }
        }
    }
}
