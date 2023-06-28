using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests_Program
{
    public class BroadcastBattery : BroadcastReceiver
    {
        TextView tv;
        Context context;
        public BroadcastBattery()
        {
        }
        public BroadcastBattery(TextView tv)
        {
            this.tv = tv;
        }
        public override void OnReceive(Context context, Intent intent)
        {
            int battery = intent.GetIntExtra("level", 0);

            tv.Text = "" + battery + "%";
            Toast.MakeText(context, "Your battery percentage is " + battery.ToString(), ToastLength.Long).Show();



        }

    }
}