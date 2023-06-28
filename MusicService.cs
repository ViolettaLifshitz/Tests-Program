using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tests_Program
{
    [Service(Label = "MusicService")]//write service to menifest file 
    [IntentFilter(new String[] { "com.violetta.MyService" })]
    public class MusicService : Service
    {
        int counter = 3;
        ServiceMusicHandler myHandler;
        IBinder binder;//null not in bagrut 
        MediaPlayer mp;

        public override void OnCreate()
        {
            base.OnCreate();
            myHandler = new ServiceMusicHandler(this);
        }

        public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId)
        {
            // start your service logic here

            mp = MediaPlayer.Create(this, Resource.Raw.LondonMarathon);
            mp.Start();
            // Return the correct StartCommandResult for the type of service you are building
            return StartCommandResult.NotSticky;

        }

        public override void OnDestroy()
        {

            base.OnDestroy();
            if (mp != null)
            {
                mp.Stop();
                mp.Release();
                mp = null;
            }
        }
        public override Android.OS.IBinder OnBind(Android.Content.Intent intent)
        {
            return null;
        }

        public void Run()
        {
            while (counter > 0)
            {

                //Toast.MakeText(this, "The demo service has started", ToastLength.Long).Show();
                Thread.Sleep(5000);
                Message message = new Message();
                message.Arg1 = counter;
                myHandler.SendMessage(message);
                counter--;
                //Toast.MakeText(this, "The demo service has competed", ToastLength.Long).Show();
            }
            StopSelf();
        }








        public class FirstServiceBinder : Binder
        {
            readonly MusicService service;

            public FirstServiceBinder(MusicService service)
            {
                this.service = service;
            }

            public MusicService GetFirstService()
            {
                return service;
            }
        }
    }
}