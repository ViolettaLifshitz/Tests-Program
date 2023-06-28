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
    [Activity(Label = "MyMessagesActivity")]
    public class MyMessagesActivity : Activity
    {
        Button back, logout, settings;
        Intent intentAddMessageToSend;
        public static List<AddMessageToFirebase> messagesList;
        public static ListView lv2;
        ListenerMessage messageListener;
        AdapterListenerMyMessages messageListenerAdapter;
        public static List<AddMessageToFirebase> messagesUserList;
        AdapterMyMessages messageAdapter;
        public static EditText title, toWhoTheMassageIsSent, content;
        public static TextView date;
        Spinner bar;
        string type;
        Dictionary<string, List<string>> dict;

      
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.my_messages_layout);
            messageAdapter = new AdapterMyMessages(this, ProceedActivity.messageList);
            lv2 = FindViewById<ListView>(Resource.Id.listView1);
            lv2.Adapter = messageAdapter;
            back = FindViewById<Button>(Resource.Id.buttonBack);
            settings = FindViewById<Button>(Resource.Id.buttonSettings);
            logout = FindViewById<Button>(Resource.Id.buttonLogout);
            logout.Click += Logout_Click;
            settings.Click += Settings_Click;
            back.Click += Back_Click;
            messagesUserList = new List<AddMessageToFirebase>();
            dict = new Dictionary<string, List<string>>()
            {
                { "date",new List<string>(){ } },{"title",new List<string>(){ }}
                ,{"content",new List<string>(){ }} ,{"email",new List<string>(){}}
            };
            messageListener = new ListenerMessage();
            messageListener.OnMessageRetrieved += MessageListener_OnMessageRetrieved;
            bar = FindViewById<Spinner>(Resource.Id.bar);
            bar.ItemSelected += Bar_ItemSelected;
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.bar_array, Android.Resource.Layout.SimpleSpinnerDropDownItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            bar.Adapter = adapter;
            // Create your application here
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SettingActivity));
            StartActivity(intent);
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
        private void Bar_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            type = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, type, ToastLength.Long).Show();
            if (type == "messages for me")
            {
                Intent intent = new Intent(this, typeof(MessagesForMeActivity));
                intent.PutExtra("source", "Tests");
                StartActivity(intent);
            }
            else if (type == "add test")
            {
                Intent intent = new Intent(this, typeof(AddTestActivity));
                intent.PutExtra("source", "Tests");
                StartActivity(intent);
            }
            else if (type == "main page")
            {
                Intent intent = new Intent(this, typeof(MenuActivity));
                intent.PutExtra("source", "Tests");
                StartActivity(intent);
            }
            else if (type == "all tests")
            {
                Intent intent = new Intent(this, typeof(AllTestsActivity));
                intent.PutExtra("source", "Tests");
                StartActivity(intent);
            }
            else if (type == "my messages")
            {
                Toast.MakeText(this, "You are already in this page", ToastLength.Long).Show();
            }
            else if (type == "add message")
            {
                Intent intent = new Intent(this, typeof(AddMessageActivity));
                intent.PutExtra("source", "Tests");
                StartActivity(intent);
            }
        }

   




        private void MessageListener_OnMessageRetrieved(object sender, ListenerMessage.MessageEventArgs e)
        {
            messagesList = e.messages;
            foreach (var item in messagesList)
            {
                if (item.GetEmail() == LoginActivity.emailText.Text)
                {


                    if (dict["date"].Count != 0)
                    {
                        if (!(messagesUserList.Contains(item)&& dict["date"].Contains(item.GetDate()) && dict["title"].Contains(item.GetTitle()) && dict["content"].Contains(item.GetContent()) && dict["email"].Contains(item.GetToWhoTheMassageIsSent())))
                        {
                            messagesUserList.Add(item);
                        }
                    }
                    else
                    {
                        
                        messagesUserList.Add(item);
                    }
                    dict["date"].Add(item.GetDate());
                    dict["title"].Add(item.GetTitle());
                    dict["content"].Add(item.GetContent());
                    dict["email"].Add(item.GetToWhoTheMassageIsSent());
                }
            }
            messageListenerAdapter = new AdapterListenerMyMessages(this, messagesUserList);
            lv2.Adapter = messageListenerAdapter;
            messageListenerAdapter.NotifyDataSetChanged();
        }


    }
}
