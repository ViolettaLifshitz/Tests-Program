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
using Java.Util;
using System.Threading;
using System.Threading.Tasks;
using AndroidX.AppCompat.App;

namespace Tests_Program
{
    [Activity(Label = "AllTestsActivity")]
    public class AllTestsActivity : Activity
    {

        public static ListView lv2;
        Spinner bar;
        string type;
        Button back, logout, settings;

    

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.all_tests_layout);
            lv2 = FindViewById<ListView>(Resource.Id.listView2);
            back = FindViewById<Button>(Resource.Id.btnBack);
            settings = FindViewById<Button>(Resource.Id.buttonSettings);
            logout = FindViewById<Button>(Resource.Id.buttonLogout);
            ProceedActivity.testsAdapter = new AdapterAllTests(this, ProceedActivity.testsList);
            lv2.Adapter = ProceedActivity.testsAdapter;
            bar = FindViewById<Spinner>(Resource.Id.bar);
            bar.ItemSelected += Bar_ItemSelected;
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.bar_array, Android.Resource.Layout.SimpleSpinnerDropDownItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            bar.Adapter = adapter;
            back.Click += Back_Click;
            settings.Click += Settings_Click;
            logout.Click += Logout_Click;
  
        }

 

        private void Logout_Click(object sender, EventArgs e)
        {
            UserLogout();

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
        private void Settings_Click(object sender, EventArgs e)
        {
  
                Intent intent = new Intent(this, typeof(SettingActivity));
                intent.PutExtra("source", "Tests");
                StartActivity(intent);
            
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        private void Bar_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            type = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, type, ToastLength.Long).Show();
            if (type == "my messages")
            {
                if (Intent.GetStringExtra("source") != null && Intent.GetStringExtra("source") == "MyMessages")
                {
                    this.Finish();
                }
                else
                {
                    Intent intent = new Intent(this, typeof(MyMessagesActivity));
                    intent.PutExtra("source", "Tests");
                    StartActivity(intent);
                }
            }
            else if (type == "add test")
            {
       
                    Intent intent = new Intent(this, typeof(AddTestActivity));
                    intent.PutExtra("source", "Tests");
                    StartActivity(intent);
                

            }
            else if (type == "messages for me")
            {
                if (Intent.GetStringExtra("source") != null && Intent.GetStringExtra("source") == "MessagesForMe")
                {
                    this.Finish();
                }
                else
                {
                    Intent intent = new Intent(this, typeof(MessagesForMeActivity));
                    intent.PutExtra("source", "Tests");
                    StartActivity(intent);
                }
            }
            else if (type == "main page")
            {
                if (Intent.GetStringExtra("source") != null && Intent.GetStringExtra("source") == "Menu")
                {
                    this.Finish();
                }
                else
                {
                    Intent intent = new Intent(this, typeof(MenuActivity));
                    intent.PutExtra("source", "Tests");
                    StartActivity(intent);
                }
            }
            else if(type== "add message")
            {
                Intent intent = new Intent(this, typeof(AddMessageActivity));
                StartActivity(intent);
            }
            else if (type == "all tests")
            {
                Toast.MakeText(this, "you are already in this page", ToastLength.Long).Show();

            }
        }



  
    

 

      
    }
}
