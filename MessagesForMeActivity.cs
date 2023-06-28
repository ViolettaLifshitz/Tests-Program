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
    [Activity(Label = "MessagesForMeActivity")]
    public class MessagesForMeActivity : Activity
    {

        public static ListView lv3;
        ListenerMessage messageListener;
        AdapterMessagesForMe messageAdapter;
        Button back,settings,logout;
        Spinner bar;
        string type;
        Dictionary<string, List<string>> dict;

 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.message_someone_sent_to_me_layout);
            lv3 = FindViewById<ListView>(Resource.Id.listView1);
            back = FindViewById<Button>(Resource.Id.buttonBack);
            settings = FindViewById<Button>(Resource.Id.buttonSettings);
            logout = FindViewById<Button>(Resource.Id.buttonLogout);
            logout.Click += Logout_Click ;
            settings.Click += Settings_Click;
            back.Click += Back_Click;
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
            intent.PutExtra("source", "MyMessages");
            StartActivity(intent);
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            UserLogout();
        }

        private void Bar_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            type = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, type, ToastLength.Long).Show();
            if (type == "my messages")
            {
                Intent intent = new Intent(this, typeof(MyMessagesActivity));
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
            else if(type== "messages for me")
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
        public bool CheckIsTheSameId(List<AddMessageToFirebase>list,string id)
        {
            foreach(var item in list)
            {
                if (item.GetMessageId() == id)
                {
                    return true;
                }
            }
            return false;
        }

        private void MessageListener_OnMessageRetrieved(object sender, ListenerMessage.MessageEventArgs e)
        {

            if (ProceedActivity.messagesFromFirebase != null)
            {
                foreach (var item in e.messages)
                {
                    if (!CheckIsTheSameId(ProceedActivity.messagesFromFirebase,item.GetMessageId()))
                    {
                        ProceedActivity.messagesFromFirebase.Add(item);
                    }
                   
                }
            }
            else
            {
                ProceedActivity.messagesFromFirebase = e.messages;
            }
            foreach (var item in ProceedActivity.messagesFromFirebase)
            {
                if (item.GetToWhoTheMassageIsSent() == LoginActivity.emailText.Text)
                {

                   if (!CheckIsTheSameId(ProceedActivity.messagesToShow,item.GetMessageId()))
                    {
                        if (dict["date"].Count != 0)
                        {
                            if (!(dict["date"].Contains(item.GetDate()) && dict["title"].Contains(item.GetTitle()) && dict["content"].Contains(item.GetContent()) && dict["email"].Contains(item.GetEmail())))
                            {
                                ProceedActivity.messagesToShow.Add(item);                    //"gfdgsdfgfdgs"  "dfsdfs"
                            }
                        }
                        else
                        {
                            ProceedActivity.messagesToShow.Add(item);
                        }
                        dict["date"].Add(item.GetDate());           //"dfsdfs"
                        dict["title"].Add(item.GetTitle());
                        dict["content"].Add(item.GetContent());
                        dict["email"].Add(item.GetEmail());
                    }
                    
                }
            }
            messageAdapter = new AdapterMessagesForMe(this, ProceedActivity.messagesToShow);
            lv3.Adapter = messageAdapter;
            messageAdapter.NotifyDataSetChanged();
        }
    }
}