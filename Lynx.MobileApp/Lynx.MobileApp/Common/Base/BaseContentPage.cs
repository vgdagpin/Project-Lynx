using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Lynx.Interfaces;
using TasqR;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.Common.Base
{
    public abstract class BaseContentPage : ContentPage
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

        protected T GetService<T>()
        {
            return App.ServiceProvider.GetService<T>();
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
    }
}
