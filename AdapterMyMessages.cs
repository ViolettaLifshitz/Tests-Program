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
    public class AdapterMyMessages : BaseAdapter<Messages>
    {
        List<Messages> messages;
        Context context;
        ImageView cartImage;
        EditText title, content, toWhoTheMassageIsSent;
        TextView date;
        Messages message;
        string userEmail;
        ImageButton sendOrRemove;
        public AdapterMyMessages(Context context, List<Messages> messages)
        {
            this.context = context;
            this.messages = messages;
        }
        public List<Messages> GetList()
        {
            return this.messages;
        }
        public void AddMessage(Messages message)
        {
            this.messages.Add(message);
            NotifyDataSetChanged();
        }
        public void DeleteMessage(int position)
        {
            this.messages.RemoveAt(position);
            NotifyDataSetChanged();
        }
        public override Messages this[int position] => this.messages[position];
        public override int Count
        {
            get { return this.messages.Count; }
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            LayoutInflater layoutInflater = ((MyMessagesActivity)context).LayoutInflater;
            View view = layoutInflater.Inflate(Resource.Layout.single_message_layout, parent, false);
            // Button btnAddP = view.FindViewById<Button>(Resource.Id.btnAddP);
            //  Button btnRemove = view.FindViewById<Button>(Resource.Id.btnRemove);
            title = view.FindViewById<EditText>(Resource.Id.textTitle);
            date = view.FindViewById<TextView>(Resource.Id.textDate);
            toWhoTheMassageIsSent = view.FindViewById<EditText>(Resource.Id.textTo);
            content = view.FindViewById<EditText>(Resource.Id.textContent);
            ImageButton back = view.FindViewById<ImageButton>(Resource.Id.buttonBack);
            back.Visibility = ViewStates.Invisible;
            message = ProceedActivity.messageList[position];    
            sendOrRemove = view.FindViewById<ImageButton>(Resource.Id.buttonSend);
            sendOrRemove.SetImageBitmap(BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.deleteButton));
            sendOrRemove.Click += SendOrRemove_Click;
            cartImage = new ImageView(context);
            if (message != null)
            {
                cartImage.SetImageBitmap(AddTestActivity.someTest);
                title.Text = " " + message.GetTitle();
                date.Text = " " + message.GetDate();
                toWhoTheMassageIsSent.Text = " " + message.GetToWhoTheMessageIsSent();
                content.Text = " " + message.GetContent() + "\nFrom:" + message.GetEmail();
                userEmail = message.GetEmail();
            }
            return view;
        }

        private void SendOrRemove_Click(object sender, EventArgs e)
        {
            int pos = (int)sendOrRemove.Tag;
            DeleteMessage(pos);
        }

        public Messages Messages
        {
            get => default;
            set
            {
            }
        }
    }

}