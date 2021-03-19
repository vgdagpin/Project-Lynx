using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Domain.ViewModels;
using Lynx.MobileApp.Handlers.Queries.BillPaymentStepsTemplateQrs;
using Lynx.Queries.BillPaymentStepsTemplateQrs;
using TasqR;
using Xamarin.Forms;

namespace Lynx.MobileApp.Controls
{
    public class PayByMyselfStepsSearchHandler : SearchHandler
    {
        private CancellationTokenSource cancellationTokenSource;
        public delegate void SearchStart();
        public delegate void SearchEnd();
        public delegate void SelectedSearchItem(BillPaymentStepsTemplateSummaryVM item, object sender);

        public event SearchStart OnSearchStart;
        public event SearchEnd OnSearchEnd;
        public event SelectedSearchItem OnSelectedSearchItem;

        public int SearchDelay { get; set; }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                if (newValue.Trim().Length >= 3)
                {
                    // Cancels the current execution
                    if (cancellationTokenSource != null) cancellationTokenSource.Cancel();

                    cancellationTokenSource = new CancellationTokenSource();

                    Task.Run(async () =>
                    {
                        var token = cancellationTokenSource.Token;
                        await Task.Delay(SearchDelay);

                        if (!token.IsCancellationRequested)
                        {
                            OnSearchStart?.Invoke();
                            ItemsSource = await LynxDependencyService.Get<ITasqR>()
                               .UsingAsHandler<GetBillPaymentStepsTemplatesQrHandler_API>()
                               .RunAsync(new GetBillPaymentStepsTemplatesQr(3, newValue.Trim()), token);
                            OnSearchEnd?.Invoke();
                        }
                    }, cancellationTokenSource.Token);
                }
            }
        }

        protected override void OnItemSelected(object item)
        {
            base.OnItemSelected(item);


            OnSelectedSearchItem?.Invoke((BillPaymentStepsTemplateSummaryVM)item, this);
        }
    }
}