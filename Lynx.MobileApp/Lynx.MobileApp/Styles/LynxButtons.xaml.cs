using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.Portable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LynxButtons : ResourceDictionary
    {
        public LynxButtons()
        {
            InitializeComponent();
        }
    }
}