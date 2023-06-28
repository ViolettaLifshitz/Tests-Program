using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Bumptech.Glide;
using Java.Text;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests_Program
{
    [Activity(Label = "SettingActivity")]
    public class SettingActivity : Activity
    {
        TextView tv;
        BroadcastBattery broadCastBattery;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.setting_layout);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Get our button from the layout resource,
            // and attach an event to it
            tv = FindViewById<TextView>(Resource.Id.batery);
            broadCastBattery = new BroadcastBattery(tv);
            // Create your application here
            ImageView gifBatteryView = FindViewById<ImageView>(Resource.Id.imageViewBattery);
            Glide.With(this)
                     .AsGif()
                     .Load(Resource.Drawable.
                    battery_charging) // Replace with the name of your GIF file
                     .Into(gifBatteryView);
            Button back = FindViewById<Button>(Resource.Id.buttonBack);
            Button logout = FindViewById<Button>(Resource.Id.buttonLogout);
            back.Click += Back_Click;
            logout.Click += Logout_Click;
        }
        public async void UserLogout()
        {
            User user = new User();
            if (await user.Logout())
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
        }
        private void Logout_Click(object sender, EventArgs e)
        {
            UserLogout();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        protected override void OnResume()
        {
            base.OnResume();
            RegisterReceiver(broadCastBattery, new IntentFilter(Intent.ActionBatteryChanged));
        }

        protected override void OnPause()
        {
            UnregisterReceiver(broadCastBattery);
            base.OnPause();
        }




    }
}

