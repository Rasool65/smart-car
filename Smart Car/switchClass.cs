using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;

namespace Smart_Car
{
    class SwitchClass
    {
        private static Context _context;
        private static ProgressDialog _progress;

        public SwitchClass(Context ctx)
        {
            _context = ctx;
        }
        public static bool Switcher(string carNumber, bool sw, string cmdOn, string cmdOff, string notifyOn, string notifyOff)
        {
            if (string.IsNullOrEmpty(carNumber) || (carNumber.Length != 11))
            {
                sw = !sw;
                return sw;
            }
            else
            {
                if (sw)
                {
                    Send(carNumber, cmdOn);
                    Toast.MakeText(_context, notifyOn, ToastLength.Long).Show();
                }
                else
                {
                    Send(carNumber, cmdOff);
                    Toast.MakeText(_context, notifyOff, ToastLength.Long).Show();
                }
                return sw;
            }
        }

        public static bool Button(string carNumber, string cmdClick, string notifyClick)
        {
            if ((string.IsNullOrEmpty(carNumber)) || (carNumber.Length != 11))
            {
                return false;
            }
            else
            {
                Send(carNumber, cmdClick);
                Toast.MakeText(_context, notifyClick, ToastLength.Long).Show();
                return true;
            }
        }

        private static void Send(string number, string message)
        {
            StartProgress();
            SmsManager sms = SmsManager.Default;
            sms.SendTextMessage(number, null, message, null, null);
        }

        private static void StartProgress()
        {
            _progress = ProgressDialog.Show(_context, "فرمان مورد نظر صادر شد!", "لطفأ منتظر دریافت پاسخ باشید...",true,false);
        }
        public static void StopProgress()
        {
            if (_progress!=null)
            {
                if (_progress.IsShowing)
                {
                    // _progress.Cancel();
                    _progress.Dismiss();
                }
            }
           
        }

    }
}