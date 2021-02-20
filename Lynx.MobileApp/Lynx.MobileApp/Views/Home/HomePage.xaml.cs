using System;
using System.IO;
using System.Threading.Tasks;
using Lynx.MobileApp.Common.Constants;
using Lynx.MobileApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await Task.Run(() =>
            {
                (BindingContext as HomePageViewModel).LoadData.Execute(null);
            });
        }


        private async void Test2Btn_Clicked(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (File.Exists(SQLiteConstants.FilePath))
                    {
                        File.Delete(SQLiteConstants.FilePath);
                    }

                    App.ServiceProvider.GetService<DbContext>().Database.Migrate();

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await DisplayAlert("Done", "Created", "OK");
                    });
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await DisplayAlert("Error", ex.InnermostException().Message, "OK");
                    });
                }
            });
        }

        private async void Test1Btn_Clicked(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                (BindingContext as HomePageViewModel).LoadData.Execute(null);
            });
        }

        private async void Test3Btn_Clicked(object sender, EventArgs e)
        {
            bool dbCreated = File.Exists(SQLiteConstants.FilePath);

            await DisplayAlert("Alert", $"Created: {dbCreated} - {SQLiteConstants.FilePath}", "OK");
        }
    }
}