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
using Bumptech.Glide;

namespace Tests_Program
{
    [Activity(Label = "MenuActivity", Theme = "@style/AppTheme")]
    public class MenuActivity : Activity
    {

        Button  allTests, myMessages, messagesForMe, logout, settings, addTest;
        ListenerUser userListener;
        public static List<User> usersList;
        public static Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>()
            {
                { "students",new List<string>(){ } },{"teachers",new List< string>()}

            };
        public static List<User> userList = new List<User>();
        public static List<string> usersEmails = new List<string>();


        


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.menu_layout);
            ImageView gifWelcomeView = FindViewById<ImageView>(Resource.Id.welcome_image);
            Glide.With(this)
                     .AsGif()
                     .Load(Resource.Drawable.
                     welcomegif) // Replace with the name of your GIF file
                     .Into(gifWelcomeView);
            ImageView gifTestView = FindViewById<ImageView>(Resource.Id.toTestProgram_image);
            Glide.With(this)
                     .AsGif()
                     .Load(Resource.Drawable.
                    toTestProgramGif) // Replace with the name of your GIF file
                     .Into(gifTestView);
            if(!usersEmails.Contains("choose to who you want send message"))
            {
                usersEmails.Add("choose to who you want send message");
            }
            Intent intentMusic = new Intent(this, typeof(MusicService));
            StartService(intentMusic);
            allTests = FindViewById<Button>(Resource.Id.allTests);
            myMessages = FindViewById<Button>(Resource.Id.buttonMyMessages);
            messagesForMe = FindViewById<Button>(Resource.Id.buttonMessagesForMe);
            userListener = new ListenerUser();
            userListener.OnUsersRetrieved += UserListener_OnUsersRetrieved;
            settings = FindViewById<Button>(Resource.Id.buttonSettings);
            settings.Click += Settings_Click;
            myMessages.Click += MyMessages_Click;
            messagesForMe.Click += MessagesForMe_Click;
            allTests.Click += AllTests_Click;
            addTest = FindViewById<Button>(Resource.Id.addTest);
            addTest.Click += AddTest_Click;
            logout = FindViewById<Button>(Resource.Id.buttonLogout);
            logout.Click += Logout_Click;
            ImageButton addMessage = FindViewById<ImageButton>(Resource.Id.addMessageButton);
            addMessage.Click += AddMessage_Click;
        }

        private void AddMessage_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AddMessageActivity));
            intent.PutExtra("source", "Menu");
            StartActivity(intent);
        }

        private void UserListener_OnUsersRetrieved(object sender, ListenerUser.UsersEventArgs e)
        {
            usersList = e.users;
   
                foreach (var item in usersList)
                {
                    if (item.GetTypeUser() == "student")
                    {
                            if (!dict["students"].Contains(item.GetEmail()))
                            {
                                dict["students"].Add(item.GetEmail());
                            }
                    }
                    else if (item.GetTypeUser() == "teacher")
                    {
                            if (!dict["teachers"].Contains(item.GetEmail()))
                            {
                                dict["teachers"].Add(item.GetEmail());
                            }
                    }
                }           
            
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            UserLogout();
        }

        private void AddTest_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AddTestActivity));
            intent.PutExtra("source", "Menu");
            StartActivity(intent);
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SettingActivity));
            intent.PutExtra("source", "Menu");
            StartActivity(intent);
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
   

        private void MessagesForMe_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MessagesForMeActivity));
            intent.PutExtra("source", "Menu");
            StartActivity(intent);
        }

        private void MyMessages_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MyMessagesActivity));
            intent.PutExtra("source", "Menu");
            StartActivity(intent);
        }

        private void AllTests_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AllTestsActivity));
            intent.PutExtra("source", "Menu");
            StartActivity(intent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
