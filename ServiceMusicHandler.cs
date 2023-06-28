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
    public class ServiceMusicHandler : Handler
    {
        int counter = 0;
        Context context;

        public ServiceMusicHandler()
        {

        }
        public ServiceMusicHandler(Context context)
        {
            this.context = context;
        }

        public  void HandleMessage(Message msg)
        {
            Toast.MakeText(context, "" + msg.Arg1, ToastLength.Short).Show();
        }
    }
}