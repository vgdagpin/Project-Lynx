﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.MobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SessionVerificationPage : ContentPage
    {
        public SessionVerificationPage()
        {
            InitializeComponent();

            BindingContext = App.ServiceProvider.GetService<SessionVerificationViewModel>();
        }
    }
}