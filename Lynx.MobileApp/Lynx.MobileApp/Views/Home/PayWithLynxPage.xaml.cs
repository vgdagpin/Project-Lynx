using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lynx.Constants;
using Lynx.MobileApp.Handlers.Queries.TextTemplateQrs;
using Lynx.Queries.TextTemplateQrs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PayWithLynxPage : LynxContentPage
    {
        #region PayWithLynxTermsOfService
        private string payWithLynxTOS;
        public string PayWithLynxTermsOfService
        {
            get => payWithLynxTOS;
            set => SetProperty(ref payWithLynxTOS, value);
        }
        #endregion

        #region IAgree
        private bool iAgree;
        public bool IAgree
        {
            get => iAgree;
            set => SetProperty(ref iAgree, value);
        }
        #endregion



        public PayWithLynxPage()
        {
            InitializeComponent();

            BindingContext = this;

            PayWithLynxTermsOfService = "Loading..";

            GetTOS();
        }

        private void GetTOS()
        {
            Task.Run(async () =>
            {
                var tt = await TasqR
                    .UsingAsHandler<FindTextTemplateQrHandler_API>()
                    .RunAsync(new FindTextTemplateQr(TextTemplateCodeConstants.PayWithLynxTermsOfService));

                PayWithLynxTermsOfService = tt.Content;
                IsLoaded = true;
            });
        }
    }
}