﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Lynx.Common.ViewModels;
using Lynx.Domain.Models;
using Lynx.MobileApp.Handlers.Queries.UserBillQrs;
using Lynx.Queries.UserBillQrs;
using Newtonsoft.Json;
using TasqR;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.ViewModels
{
    [QueryProperty(nameof(UserBillID), nameof(UserBillID))]
    public class UserBillDetailViewModel : LynxViewModel
    {

        #region UserBillID
        private Guid billID;
        public string UserBillID
        {
            get => billID.ToString();
            set
            {
                billID = Guid.Parse(value);

                LoadItemId(billID);
            }
        }
        #endregion

        #region UserBill
        private UserBillBO userBill;
        public UserBillBO UserBill
        {
            get { return userBill; }
            set { SetProperty(ref userBill, value); }
        }
        #endregion

        #region IsPaid
        private bool isPaid;
        public bool IsPaid
        {
            get { return isPaid; }
            set { SetProperty(ref isPaid, value); }
        }
        #endregion

        #region UserBillJson
        private string userBillJson;
        public string UserBillJson
        {
            get => userBillJson;
            set => SetProperty(ref userBillJson, value);
        }
        #endregion



        public ICommand PayWithLynx { get; }
        public ICommand PayByMyself { get; }




        public UserBillDetailViewModel()
        {
            PayWithLynx = new Command(async () => await Shell.Current.GoToPayWithLynxPage(billID));
            PayByMyself = new Command(async () => await Shell.Current.GoToPayByMyselfPage(billID));
        }

        private void LoadItemId(Guid billID)
        {
            IsBusy = true;
            IsLoaded = false;

            Task.Run(async () =>
            {
                try
                {

                    UserBill = await TasqR
                        .UsingAsHandler<FindUserBillQrHandler_API>()
                        .RunAsync(new FindUserBillQr(billID));

                    UserBillJson = JsonConvert.SerializeObject(UserBill, Formatting.Indented);
                }
                catch (Exception ex)
                {
                    LogError(ex);
                }

                IsLoaded = true;
                IsBusy = false;
            });
        }
    }
}