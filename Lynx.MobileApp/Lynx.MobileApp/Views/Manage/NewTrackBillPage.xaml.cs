using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lynx.Domain.ViewModels;
using Lynx.MobileApp.ViewModels.Manage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.Views.Manage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTrackBillPage : ContentPage
    {
        public NewTrackBillPage()
        {
            InitializeComponent();

            var bindingContext = ((NewTrackBillViewModel)BindingContext);

            bindingContext.LoadBillsCommand.Execute(null);

            bindingContext.OnRecordCreateResultEvent += bindingContext_OnRecordCreateResultEvent;
        }

        private void bindingContext_OnRecordCreateResultEvent(CreateResult<TrackBillVM> createResult, object sender)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (createResult.IsCreated == true)
                {
                    await Shell.Current.Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", createResult.Error.Message, "OK");
                }
            });
        }
    }
}