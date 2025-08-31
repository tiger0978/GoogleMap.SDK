using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Timer = System.Threading.Timer;


namespace GooleMap.SDK.Core.Utility.Extensions
{
    public static class DebounceExtension
    {
        public static Timer _timer = null;
        public static Action<object> _callback = null;
        public static String input = "";
        static Control control = null;

        public static void DebounceTime(this Control form, Action<object> callback, object state, int dueTime)
        {
            control = form;
            _callback = callback;
            input = state?.ToString();
            if (_timer != null)
            {

                _timer.Change(dueTime, -1);
                return;
            }
            _timer = new Timer(TriggerCallback, null, dueTime, -1);

        }
        public static void TriggerCallback(object state)
        {
            control.Dispatcher.Invoke((Action)(() => {
                _callback(input);
            }));
        }
    }
}
