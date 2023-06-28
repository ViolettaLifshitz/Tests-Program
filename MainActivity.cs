using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Android.Content;
using Android.Widget;
using System.Collections.Generic;
using Android;
using Bumptech.Glide;

namespace Tests_Program
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ImageButton login, register, settings;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            ImageView gifImageView = FindViewById<ImageView>(Resource.Id.welcome_image);
            Glide.With(this)
                     .AsGif()
                     .Load(Resource.Drawable.
                     welcomegif) // Replace with the name of your GIF file
                     .Into(gifImageView);
            ImageView gifTestView = FindViewById<ImageView>(Resource.Id.toTestProgram_image);
            Glide.With(this)
                     .AsGif()
                     .Load(Resource.Drawable.
                    toTestProgramGif) // Replace with the name of your GIF file
                     .Into(gifTestView);
            Intent intent = new Intent(this, typeof(MusicService));
            StartService(intent);
            login = FindViewById<ImageButton>(Resource.Id.login);
            register = FindViewById<ImageButton>(Resource.Id.register);
            settings = FindViewById<ImageButton>(Resource.Id.setting);
            settings.Visibility = ViewStates.Invisible;
            login.Click += Login_Click;
            register.Click += Register_Click;

        }



        private void Register_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RegisterActivity));
            StartActivity(intent);
        }

        private void Login_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
