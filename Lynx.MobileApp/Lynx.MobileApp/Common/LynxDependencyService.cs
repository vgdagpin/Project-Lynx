using System;
using System.Collections.Generic;
using System.Text;
using TasqR;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp
{
    public static class LynxDependencyService
    {
        public static T Get<T>() where T : class
        {
            var result = App.ServiceProvider.GetService<T>();

            if (result == null)
            {
                DependencyService.Get<T>();
            }

            return result;
        }
    }
}