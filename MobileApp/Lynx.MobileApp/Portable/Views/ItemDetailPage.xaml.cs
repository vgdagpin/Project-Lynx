using Lynx.MobileApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Lynx.MobileApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}