using Android.App;
using Android.Content;
using Android.Graphics;
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
    [Activity(Label = "AddMessageActivity")]
    public class AddMessageActivity : Activity
    {
        Messages messageToSend;
        public static AddMessageToFirebase messageToAdd;
        public static EditText title, toWhoTheMassageIsSent, content;

        public static TextView date;
        ImageButton buttonSend;
        string type;
        Spinner toWhoTheMessageIsSentSpinner;
        Spinner spinnerMessage;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.single_message_layout);
            // Create your application here

            title = FindViewById<EditText>(Resource.Id.textTitle);
            date = FindViewById<TextView>(Resource.Id.textDate);

            toWhoTheMassageIsSent = FindViewById<EditText>(Resource.Id.textTo);

            content = FindViewById<EditText>(Resource.Id.textContent);
            Toast.MakeText(this, "ok" + title.Text + date.Text, ToastLength.Long).Show();
            buttonSend = FindViewById<ImageButton>(Resource.Id.buttonSend);
            buttonSend.Click += ButtonSend_Click;
            ImageButton buttonBack = FindViewById<ImageButton>(Resource.Id.buttonBack);
            buttonBack.SetImageBitmap(BitmapFactory.DecodeResource(this.Resources, Resource.Drawable.back_button));
            buttonBack.Click += ButtonBack_Click;
            foreach (var item in MenuActivity.dict["teachers"])
            {
                if(!MenuActivity.usersEmails.Contains(item))
                    MenuActivity.usersEmails.Add(item);
            }

            if (MenuActivity.dict["teachers"].Contains(LoginActivity.emailText.Text))
            {
                foreach (var item in MenuActivity.dict["students"])
                {
                    if (!MenuActivity.usersEmails.Contains(item))
                        MenuActivity.usersEmails.Add(item);
                }
            }
            toWhoTheMessageIsSentSpinner = FindViewById<Spinner>(Resource.Id.toWhoTheMessageIsSent);
            toWhoTheMessageIsSentSpinner.ItemSelected += ToWhoTheMessageIsSentSpinner_ItemSelected;
            var adapter2 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, MenuActivity.usersEmails);
            //adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            toWhoTheMessageIsSentSpinner.Adapter = adapter2;
        
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        private async void ButtonSend_Click(object sender, EventArgs e)
        {
            if (type == "choose to who you want send message")
            {
                Toast.MakeText(this, "invalid email user,please choose to who you want to send the message", ToastLength.Long).Show();
            }
            else


            {
                messageToSend = new Messages(toWhoTheMassageIsSent.Text, title.Text, LoginActivity.emailText.Text, content.Text);
                messageToAdd = new AddMessageToFirebase(toWhoTheMassageIsSent.Text, title.Text, LoginActivity.emailText.Text, content.Text);
                try
                {
                    if (await messageToAdd.NewMessage())
                    {
                        ProceedActivity.messageList.Add(messageToSend);
                        MyMessagesActivity.messagesUserList.Add(messageToAdd);
                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "" + ex.Message, ToastLength.Long).Show();
                }
                if (toWhoTheMassageIsSent.Text == LoginActivity.emailText.Text)
                {

                    ProceedActivity.messagesFromFirebase.Add(messageToAdd);

                }
                if (Intent.GetStringExtra("source") != null && Intent.GetStringExtra("source") == "MyMessages")
                {
                    this.Finish();
                }
                else
                {
                    Intent intent = new Intent(this, typeof(MyMessagesActivity));
                    StartActivity(intent);
                }
            }
        }

        private void ToWhoTheMessageIsSentSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            spinnerMessage = (Spinner)sender;
            type = string.Format("{0}", spinnerMessage.GetItemAtPosition(e.Position));
            if (Intent.GetStringExtra("source") != null && Intent.GetStringExtra("source") == "intentAddCommentMessage")
            {
                toWhoTheMassageIsSent.Text = AdapterMessagesForMe.message.GetEmail();
            }
            else
            {
                toWhoTheMassageIsSent.Text = type;

            }
        }



     
        

       
    }
}
