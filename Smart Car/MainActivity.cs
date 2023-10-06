using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Telephony.Gsm;
using Object = Java.Lang.Object;

namespace Smart_Car
{
    [Activity(Label = "Smart Car", MainLauncher = true,LaunchMode = LaunchMode.SingleTop,Icon = "@drawable/car"/*,NoHistory = true*/)]
    public class MainActivity : Activity
    {
        private static EditText txtCarNumber;
        private Switch swDoors;
        private static Switch swMasterPower;
        private static Switch swMainPower;
        private static Switch swAC;
        private Button btnStart;
        private Button btnTurnOn;
        private Button btnTurnOff;
        private static ImageView imgDoors;
        private static ImageView imgMasterPower;
        private static ImageView imgMainPower;
        private static ImageView imgAC;
        private static ImageView imgStart;
        private static string _sender;
        private static string _message;
        bool _isValid = true;


        [BroadcastReceiver]
        [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" })]

        public class SmsReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Intent intent)
            {
              
                if (intent.HasExtra("pdus"))
                {
                    //DeletMessag(context);
                    //MarkAsRead(context);
                   // ClearNotifications();
                    var smsArray = (Object[])intent.Extras.Get("pdus");
                    foreach (var item in smsArray)
                    {
                        var sms = SmsMessage.CreateFromPdu((byte[])item);
                        _message = sms.MessageBody;
                        _sender = sms.OriginatingAddress;
                    }
                    if (!(string.IsNullOrEmpty(txtCarNumber.Text) || (txtCarNumber.Text.Length != 11)))
                    {
                        if (_sender.Contains(txtCarNumber.Text.Substring(1, 10)))
                        {
                            MsgReceived(_message, context);
                            SwitchClass.StopProgress();
                            var i = new Intent(context, typeof(NotificationsCommand));
                            i.PutExtra("Command", _message);
                            context.StartService(i); // show Notification
                            
                            //// here should be message Converstaion (Delete) or (Mark as read) and remove notification
                        }
                    }
                   
                }
            }
        }
        //private static void DeletMessag(Context context)
        //{
        //    try
        //    {
        //        Android.Net.Uri uriSms = Android.Net.Uri.Parse("content://sms/inbox");
        //        var cursor = context.ContentResolver.Query(uriSms, new string[] { "_id", "thread_id" },
        //                null, null, null);
        //        if (null != cursor && cursor.MoveToFirst())
        //        {
        //            do
        //            {
        //                // Delete SMS
        //                long threadId = cursor.GetLong(1);

        //                int result = context.ContentResolver.Delete(Android.Net.Uri.Parse("content://sms/inbox/" + threadId),
        //                        null, null);
        //            } while (cursor.MoveToNext());
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Toast.MakeText(context, e.Message, ToastLength.Long).Show();
        //    }
        //}

        //static void MarkAsRead(Context context)
        //{
        //    var uri = Android.Net.Uri.Parse("content://sms/inbox");
        //    var cursor = context.ContentResolver.Query(uri, null, null, null, null);
        //    try
        //    {

        //        while (cursor.MoveToNext())
        //        {
        //            if ((cursor.GetString(cursor.GetColumnIndex("address")).Equals("09126197621")) && (cursor.GetInt(cursor.GetColumnIndex("read")) == 0))
        //            {
        //                if (cursor.GetString(cursor.GetColumnIndex("body")).StartsWith("ddd"))
        //                {
        //                    string threadId = cursor.GetString(cursor.GetColumnIndex("_id"));
        //                    ContentValues values = new ContentValues();
        //                    values.Put("read", 1);
        //                    context.ContentResolver.Update(Android.Net.Uri.Parse("content://sms/inbox"), values, "_id=" + threadId, null);
        //                    return;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //    }
        //}

        protected override void OnCreate(Bundle bundle)
        {
            NotificationsCommand.ClearNotifications();
            new SwitchClass(this);
            new Notify();
            new Command();
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource//baraye in activity yek view dar nazar begir
            SetContentView(Resource.Layout.Main);

            txtCarNumber = FindViewById<EditText>(Resource.Id.txtCarNumber);

           
            swDoors = FindViewById<Switch>(Resource.Id.swDoors);
            swDoors.Checked = false;
            swMainPower = FindViewById<Switch>(Resource.Id.swMainPower);
            swMainPower.Checked = false;
            swMasterPower = FindViewById<Switch>(Resource.Id.swMasterPower);
            swMasterPower.Checked = false;
            swAC = FindViewById<Switch>(Resource.Id.swAC);
            swAC.Checked = false;

            btnStart = FindViewById<Button>(Resource.Id.btnStart);
            btnTurnOff = FindViewById<Button>(Resource.Id.btnOff);
            btnTurnOn = FindViewById<Button>(Resource.Id.btnOn);

            imgDoors = FindViewById<ImageView>(Resource.Id.imgDoors);
            imgAC = FindViewById<ImageView>(Resource.Id.imgAC);
            imgMainPower = FindViewById<ImageView>(Resource.Id.imgMainPower);
            imgMasterPower = FindViewById<ImageView>(Resource.Id.imgMasterPower);
            imgStart = FindViewById<ImageView>(Resource.Id.imgStart);

            swMasterPower.Click += SwMasterPower_Click;
            swDoors.Click += SwDoors_Click;
            swMainPower.Click += SwMainPower_Click;
            swAC.Click += SwAC_Click;


            btnStart.Click += BtnStart_Click;
            btnTurnOn.Click += BtnTurnOn_Click;
            btnTurnOff.Click += BtnTurnOff_Click;

        }

        private void SwMasterPower_Click(object sender, EventArgs e)
        {
            AlertVoid("توجه", "آیا از ارسال فرمان تغییر وضعیت روشن/خاموش شدن برق اصلی خودرو مطمئن هستید؟ ", "بله", YesMasterPower, "خیر", NoMasterPower);
        }

        private void SwMainPower_Click(object sender, EventArgs e)
        {
            AlertVoid("توجه", "آیا از ارسال فرمان تغییر وضعیت روشن/خاموش شدن برق پشت آمپر خودرو مطمئن هستید؟ ", "بله", YesMainPower, "خیر", NoMainPower);

        }

        private void SwDoors_Click(object sender, EventArgs e)
        {
            AlertVoid("توجه", "آیا از ارسال فرمان تغییر وضعیت باز/فقل شدن درب های خودرو مطمئن هستید؟ ", "بله", YesDoorCchange, "خیر", NoDoorChange);
        }

        private void SwAC_Click(object sender, EventArgs e)
        {
            AlertVoid("توجه", "آیا از ارسال فرمان تغییر وضعیت روشن/خاموش شدن کولر خودرو مطمئن هستید؟ ", "بله", SwAc, "خیر", noAc);

        }

        private void BtnTurnOff_Click(object sender, EventArgs e)
        {
            AlertVoid("توجه", "آیا از ارسال فرمان خاموش شدن خودرو مطمئن هستید؟ ", "بله", TurnOff, "خیر", delegate { });
        }
        private void TurnOff(object sender, DialogClickEventArgs e)
        {
            _isValid = SwitchClass.Button(txtCarNumber.Text, Command.TurnOff, Smart_Car.Notify.MsgTurnOff);
            Validation(_isValid);
        }

        private void BtnTurnOn_Click(object sender, EventArgs e)
        {
            AlertVoid("توجه", "آیا از ارسال فرمان روشن شدن خودرو مطمئن هستید؟ ", "بله", TurnOn, "خیر", delegate { });
        }
        private void TurnOn(object sender, DialogClickEventArgs e)
        {
            _isValid = SwitchClass.Button(txtCarNumber.Text, Command.TurnOn, Smart_Car.Notify.MsgTurnOn);
            Validation(_isValid);
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            AlertVoid("توجه", "آیا از ارسال فرمان استارت خودرو مطمئن هستید؟ ", "بله", Start, "خیر", delegate { });
        }
        private void Start(object sender, DialogClickEventArgs e)
        {
            _isValid = SwitchClass.Button(txtCarNumber.Text, Command.StartEngin, Smart_Car.Notify.MsgStartEngin);
            Validation(_isValid);
        }

        private void SwAc(object sender, DialogClickEventArgs e)
        {
            bool swNow = !swAC.Checked;
            swAC.Checked = SwitchClass.Switcher(txtCarNumber.Text, swAC.Checked, Command.AcOn, Command.AcOff, Smart_Car.Notify.MsgAcOn, Smart_Car.Notify.MsgAcOff);
            SwitchValid(swNow, swAC.Checked);
        }

        private void noAc(object sender, DialogClickEventArgs e)
        {
            swAC.Checked = !swAC.Checked;
        }
        private void NoMainPower(object sender, DialogClickEventArgs e)
        {
            swMainPower.Checked = !swMainPower.Checked;
        }
        private void YesMainPower(object sender, DialogClickEventArgs e)
        {
            bool swNow = !swMainPower.Checked;
            swMainPower.Checked = SwitchClass.Switcher(txtCarNumber.Text, swMainPower.Checked, Command.MainOn, Command.MainOff, Smart_Car.Notify.MsgMainOn, Smart_Car.Notify.MsgMainOff);
            SwitchValid(swNow, swMainPower.Checked);
        }
        private void NoMasterPower(object sender, DialogClickEventArgs e)
        {
            swMasterPower.Checked = !swMasterPower.Checked;
        }
        private void YesMasterPower(object sender, DialogClickEventArgs e)
        {
            bool swNow = !swMasterPower.Checked;
            swMasterPower.Checked = SwitchClass.Switcher(txtCarNumber.Text, swMasterPower.Checked, Command.MasterOn, Command.MasterOff, Smart_Car.Notify.MsgMasterOn, Smart_Car.Notify.MsgMasterOff);
            SwitchValid(swNow, swMasterPower.Checked);
        }
        private void NoDoorChange(object sender, DialogClickEventArgs e)
        {
            swDoors.Checked = !swDoors.Checked;
        }
        private void YesDoorCchange(object sender, DialogClickEventArgs e)
        {
            bool swNow = !swDoors.Checked;
            swDoors.Checked = SwitchClass.Switcher(txtCarNumber.Text, swDoors.Checked, Command.OpenDoors, Command.CloseDoors, Smart_Car.Notify.MsgOpenDoors, Smart_Car.Notify.MsgCloseDoors);
            SwitchValid(swNow, swDoors.Checked);
        }

        private static void MsgReceived(string msg, Context context)
        {
            switch (msg)
            {
                case "RA-D1":
                    imgDoors.SetImageResource(Android.Resource.Drawable.ButtonStarBigOn);
                    Toast.MakeText(context, Smart_Car.Notify._MsgOpenDoors, ToastLength.Long).Show();
                    break;
                case "RA-D0":
                    imgDoors.SetImageResource(Android.Resource.Drawable.ButtonStarBigOff);
                    Toast.MakeText(context, Smart_Car.Notify._MsgCloseDoors, ToastLength.Long).Show();
                    break;
                case "RA-M1":
                    imgMainPower.SetImageResource(Android.Resource.Drawable.ButtonStarBigOn);
                    Toast.MakeText(context, Smart_Car.Notify._MsgMainOn, ToastLength.Long).Show();
                    break;
                case "RA-M0":
                    imgMainPower.SetImageResource(Android.Resource.Drawable.ButtonStarBigOff);
                    Toast.MakeText(context, Smart_Car.Notify._MsgMainOff, ToastLength.Long).Show();
                    break;
                case "RA-M3":
                    imgMasterPower.SetImageResource(Android.Resource.Drawable.ButtonStarBigOn);
                    Toast.MakeText(context, Smart_Car.Notify._MsgMasterOn, ToastLength.Long).Show();
                    break;
                case "RA-M2":
                    imgMasterPower.SetImageResource(Android.Resource.Drawable.ButtonStarBigOff);
                    Toast.MakeText(context, Smart_Car.Notify._MsgMasterOff, ToastLength.Long).Show();
                    break;
                case "RA-A1":
                    imgAC.SetImageResource(Android.Resource.Drawable.ButtonStarBigOn);
                    Toast.MakeText(context, Smart_Car.Notify._MsgAcOn, ToastLength.Long).Show();
                    break;
                case "RA-A0":
                    imgAC.SetImageResource(Android.Resource.Drawable.ButtonStarBigOff);
                    Toast.MakeText(context, Smart_Car.Notify._MsgAcOff, ToastLength.Long).Show();
                    break;
                case "RA-EN":
                    // imgDoors.SetImageResource(Android.Resource.Drawable.ButtonStarBigOn);
                    Toast.MakeText(context, Smart_Car.Notify._MsgStartEngin, ToastLength.Long).Show();
                    break;
                case "RA-T0":
                    imgStart.SetImageResource(Android.Resource.Drawable.StarBigOff);
                    imgAC.SetImageResource(Android.Resource.Drawable.ButtonStarBigOff);
                    imgMainPower.SetImageResource(Android.Resource.Drawable.ButtonStarBigOff);
                    imgMasterPower.SetImageResource(Android.Resource.Drawable.ButtonStarBigOff);
                    swAC.Checked = swMainPower.Checked = swMasterPower.Checked = false;

                    Toast.MakeText(context, Smart_Car.Notify._MsgTurnOff, ToastLength.Long).Show();
                    break;
                case "RA-T1":
                    imgStart.SetImageResource(Android.Resource.Drawable.StarBigOn);
                    Toast.MakeText(context, Smart_Car.Notify._MsgTurnOn, ToastLength.Long).Show();
                    break;
                default:
                    break;
            }
        }

        private void Validation(bool valid)
        {
            if (valid)
            {
                txtCarNumber.Background = new ColorDrawable(Color.MediumSeaGreen);
            }
            else
            {
                txtCarNumber.Background = new ColorDrawable(Color.Yellow);
                txtCarNumber.Error = Smart_Car.Notify.MsgNumber;
            }
        }

        private void SwitchValid(bool swNow, bool swchecked)
        {
            if (((swNow) && (swchecked)) || ((!swNow) && (!swchecked)))
            {
                Validation(false);
            }
            else
            {
                Validation(true);
            }
        }

        private void AlertVoid(string title, string message, string positive, EventHandler<DialogClickEventArgs> yes, string negative, EventHandler<DialogClickEventArgs> no)
        {
            var alert = new AlertDialog.Builder(this);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton(positive, yes);
            alert.SetNegativeButton(negative, no);
            alert.Show();
        }

    }
}

