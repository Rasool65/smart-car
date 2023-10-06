using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Smart_Car
{

    public class Notify
    {
        public Notify()
        {
            MsgOpenDoors = "باز شدن درب ها";
            MsgCloseDoors = "فقل شدن درب ها";
            MsgMasterOn = "اتصال برق اصلی";
            MsgMasterOff = "قطع برق اصلی";
            MsgMainOn = "اتصال برق پشت آمپر";
            MsgMainOff = "قطع برق پشت آمپر";
            MsgAcOn = "اتصال برق کولر";
            MsgAcOff = "قطع برق کولر";
            MsgStartEngin = "استارت";
            MsgTurnOn = "روشن شدن خودرو";
            MsgTurnOff = "خاموش شدن خودرو";
            MsgNumber = "شماره موبایل خودرو را بدرستی وارد نمایید";

            _MsgOpenDoors = "درب ها باز شدند";
            _MsgCloseDoors = "درب ها قفل شدند";
            _MsgMasterOn = " برق اصلی وصل شد";
            _MsgMasterOff = "برق اصلی قطع شد";
            _MsgMainOn = "برق پشت آمپر وصل شد";
            _MsgMainOff = "برق پشت آمپر قطع شد";
            _MsgAcOn = "برق کولر وصل شد";
            _MsgAcOff = "برق کولر قطع شد";
            _MsgStartEngin = "استارت زده شد";
            _MsgTurnOn = "خودرو روشن شد";
            _MsgTurnOff = "خودرو خاموش شد";
        }
        public static string MsgOpenDoors { get; set; }
        public static string MsgCloseDoors { get; set; }
        public static string MsgMainOn { get; set; }
        public static string MsgMainOff { get; set; }
        public static string MsgMasterOn { get; set; }
        public static string MsgMasterOff { get; set; }
        public static string MsgAcOn { get; set; }
        public static string MsgAcOff { get; set; }
        public static string MsgStartEngin { get; set; }
        public static string MsgTurnOn { get; set; }
        public static string MsgTurnOff { get; set; }
        public static string MsgNumber { get; set; }

        public static string _MsgOpenDoors { get; set; }
        public static string _MsgCloseDoors { get; set; }
        public static string _MsgMainOn { get; set; }
        public static string _MsgMainOff { get; set; }
        public static string _MsgMasterOn { get; set; }
        public static string _MsgMasterOff { get; set; }
        public static string _MsgAcOn { get; set; }
        public static string _MsgAcOff { get; set; }
        public static string _MsgStartEngin { get; set; }
        public static string _MsgTurnOn { get; set; }
        public static string _MsgTurnOff { get; set; }

    }
}
