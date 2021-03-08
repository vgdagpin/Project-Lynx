using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lynx.MobileApp.Common.Constants;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
        }

        private async void Test2Btn_Clicked(object sender, EventArgs e)
        {
            await Task.Run(async () =>
            {
                try
                {
                    if (File.Exists(SQLiteConstants.FilePath))
                    {
                        File.Delete(SQLiteConstants.FilePath);
                        await DisplayAlert("Done", "Deleted", "OK");
                    }

                    //App.ServiceProvider.GetService<DbContext>().Database.Migrate();

                    //Device.BeginInvokeOnMainThread(async () =>
                    //{
                    //    await DisplayAlert("Done", "Created", "OK");
                    //});
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

        private async void Test3Btn_Clicked(object sender, EventArgs e)
        {
            bool dbCreated = File.Exists(SQLiteConstants.FilePath);

            await DisplayAlert("Alert", $"Created: {dbCreated} - {SQLiteConstants.FilePath}", "OK");
        }
    }
}