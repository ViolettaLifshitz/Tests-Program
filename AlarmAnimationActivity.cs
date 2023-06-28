using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tests_Program
{
    [Activity(Label = "AlarmAnimationActivity")]
    public class AlarmAnimationActivity : Activity
    {
        ImageButton buttonClose;
        private ImageView imageView;
        AdapterAllTests testsAdapter;
        ListView lv2;

        public AdapterAllTests AdapterAllTests
        {
            get => default;
            set
            {
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.alarm_animation_layout);

            imageView = FindViewById<ImageView>(Resource.Id.imageViewAnimation);
            buttonClose = FindViewById<ImageButton>(Resource.Id.buttonCloseNotification);
            buttonClose.Click += ButtonClose_Click;
            // Start the animation
            lv2 = FindViewById<ListView>(Resource.Id.listViewTests);
            testsAdapter = new AdapterAllTests(this, ProceedActivity.testsUrgent);
            lv2.Adapter = testsAdapter;
            StartAnimation();

            // Create your application here
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MenuActivity));
            StartActivity(intent);
        }

        private void StartAnimation()
        {
            AnimationDrawable animation = (AnimationDrawable)imageView.Drawable;

            // Set the animation to loop
            animation.OneShot = false;

            // Start the animation
            animation.Start();
        }

      
    }
}