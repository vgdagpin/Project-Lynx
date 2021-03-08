using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Lynx.MobileApp
{
    public class LynxCommand : Command
    {
        public LynxCommand(Action execute, Action beforeExecute = null, Action afterExecute = null) : base((Action<object>)delegate
        {
            beforeExecute?.Invoke();
            execute();
            afterExecute?.Invoke();
        })
        {

        }

        public LynxCommand(Action<object> execute) : base(execute)
        {
        }
    }
}
