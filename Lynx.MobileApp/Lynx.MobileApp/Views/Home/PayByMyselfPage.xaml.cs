using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.MobileApp.Handlers.Queries.BillPaymentStepsTemplateQrs;
using Lynx.Queries.BillPaymentStepsTemplateQrs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PayByMyselfPage : LynxContentPage
    {
        #region JsonTemplate
        private string stepsTemplate;
        public string StepsTemplate
        {
            get => stepsTemplate;
            set => SetProperty(ref stepsTemplate, value);
        }
        #endregion

        private bool IsFirstLoad = true;

        public PayByMyselfPage()
        {
            InitializeComponent();

            BindingContext = this;

            shPayByMeselfSearchTemplate.OnSearchStart += shPayByMeselfSearchTemplate_OnSearchStart;
            shPayByMeselfSearchTemplate.OnSearchEnd += shPayByMeselfSearchTemplate_OnSearchEnd;
            shPayByMeselfSearchTemplate.OnSelectedSearchItem += shPayByMeselfSearchTemplate_OnSelectedSearchItem;
        }

        private async void shPayByMeselfSearchTemplate_OnSelectedSearchItem(BillPaymentStepsTemplateSummaryVM item, object sender)
        {
            StepsTemplate = "Getting template..";

            var template = await TasqR
                .UsingAsHandler<FindBillPaymentStepsTemplateQrHandler_API>()
                .RunAsync(new FindBillPaymentStepsTemplateQr(item.ID));

            StepsTemplate = template.LongDesc;
        }

        private void shPayByMeselfSearchTemplate_OnSearchEnd()
        {
            StepsTemplate = "";
        }

        private void shPayByMeselfSearchTemplate_OnSearchStart()
        {
            StepsTemplate = "Loading..";
        }
    }
}