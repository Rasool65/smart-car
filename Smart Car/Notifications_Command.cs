using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Text;

namespace Smart_Car
{
    [Service]
    class NotificationsCommand : Service
    {
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            string command = intent.GetStringExtra("Command");

            switch (command)
            {
                case "RA-D0":
                    SetNotifyString(Smart_Car.Notify._MsgCloseDoors);
                    break;
                case "RA-D1":
                    SetNotifyString(Smart_Car.Notify._MsgOpenDoors);
                    break;
                case "RA-T0":
                    SetNotifyString(Smart_Car.Notify._MsgTurnOff);
                    break;
                case "RA-T1":
                    SetNotifyString(Smart_Car.Notify._MsgTurnOn);
                    break;
                case "RA-M0":
                    SetNotifyString(Smart_Car.Notify._MsgMainOff);
                    break;
                case "RA-M1":
                    SetNotifyString(Smart_Car.Notify._MsgMainOn);
                    break;
                case "RA-M2":
                    SetNotifyString(Smart_Car.Notify._MsgMasterOn);
                    break;
                case "RA-M3":
                    SetNotifyString(Smart_Car.Notify._MsgMasterOff);
                    break;
                case "RA-EN":
                    SetNotifyString(Smart_Car.Notify._MsgStartEngin);
                    break;
                case "RA-A0":
                    SetNotifyString(Smart_Car.Notify._MsgAcOff);
                    break;
                case "RA-A1":
                    SetNotifyString(Smart_Car.Notify._MsgAcOn);
                    break;
                default:
                    break;
            }


            return base.OnStartCommand(intent, flags, startId);
        }


        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        private void SetNotifyString(string response)
        {

            // gereftan servive notification az device , modiriat konandeye notification haye jari
            var appNotification = (NotificationManager)GetSystemService(NotificationService);

            // sakht yek notification
            var carNotification = new Notification(Resource.Drawable.car, "پاسخ دریافت شد")
            {
                Flags = NotificationFlags.HighPriority
            };
            //user betoone hazf kone ya na...

            // sakhte badaneye Notification              activity , che adadi bargardonam? , roye in click shod be koja bere?
            var notificationIntent = PendingIntent.GetActivity(this, 0, new Intent(this,typeof(MainActivity)),0);
            

            //set kardane Eventesh
#pragma warning disable 618
            carNotification.SetLatestEventInfo(this, "پاسخ دریافت شد", response, notificationIntent);
#pragma warning restore 618
            appNotification.Notify(2, carNotification);

        }


        public static void ClearNotifications()
        {
            NotificationManager manager = (NotificationManager)Application.Context.GetSystemService(NotificationService);
            manager.Cancel(2);
        }

    }

}