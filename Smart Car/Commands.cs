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
    public class Command
    {
        public Command()
        {
            OpenDoors = "RA-D1";
            CloseDoors = "RA-D0";
            MainOn = "RA-M1";
            MainOff = "RA-M0";
            MasterOff = "RA-M2";
            MasterOn = "RA-M3";
            AcOff = "RA-A0";
            AcOn = "RA-A1";
            StartEngin = "RA-EN";
            TurnOn = "RA-T1";
            TurnOff = "RA-T0";
        }
        public static string OpenDoors { get; set; }
        public static string CloseDoors { get; set; }
        public static string MainOn { get; set; }
        public static string MainOff { get; set; }
        public static string MasterOn { get; set; }
        public static string MasterOff { get; set; }
        public static string AcOn { get; set; }
        public static string AcOff { get; set; }
        public static string StartEngin { get; set; }
        public static string TurnOn { get; set; }
        public static string TurnOff { get; set; }

    }

}