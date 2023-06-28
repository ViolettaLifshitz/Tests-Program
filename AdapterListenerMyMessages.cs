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
    class AdapterListenerMyMessages : BaseAdapter<AddMessageToFirebase>
    {
        Context context;
        List<AddMessageToFirebase> messageList;
        TextView toWhoTheMassageIsSent, title, date, content;
        Button btnAdd;
        Dialog selectedPieDialog;
        ListView DtLV;
        ImageButton sendOrRemove;
        AddMessageToFirebase message;
        public AdapterListenerMyMessages(Context context, List<AddMessageToFirebase> messageList)
        {
            this.context = context;
            this.messageList = messageList;
        }

        public List<AddMessageToFirebase> GetList()
        {
            return this.messageList;
        }
        public void AddMessage(AddMessageToFirebase message)
        {
            this.messageList.Add(message);
            NotifyDataSetChanged();
        }
        public void DeleteMessage(int position)
        {
            this.messageList.RemoveAt(position);
            NotifyDataSetChanged();
        }
     
        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }
        public override int Count
        {
            get { return this.messageList.Count; }
        }
        public override long GetItemId(int position)
        {
            return position;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Android.Views.LayoutInflater layoutInflater = ((MyMessagesActivity)context).LayoutInflater;
            Android.Views.View view = layoutInflater.Inflate(Resource.Layout.single_message_layout, parent, false);
            date = view.FindViewById<TextView>(Resource.Id.textDate);
            toWhoTheMassageIsSent = view.FindViewById<TextView>(Resource.Id.textTo);
            title = view.FindViewById<TextView>(Resource.Id.textTitle);
            content = view.FindViewById<TextView>(Resource.Id.textContent);

            message = messageList[position];
            ImageButton back = view.FindViewById<ImageButton>(Resource.Id.buttonBack);
            back.Visibility = ViewStates.Invisible;

            sendOrRemove = view.FindViewById<ImageButton>(Resource.Id.buttonSend);
            sendOrRemove.SetImageBitmap(BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.deleteButton));
            sendOrRemove.Click += SendOrRemove_Click;
            sendOrRemove.Tag = position;
            if (message != null)
            {
                title.Text = " " + message.GetTitle();
                date.Text = " " + message.GetDate();
                content.Text = " " + message.GetContent() + "\n From: " + message.GetEmail();
                toWhoTheMassageIsSent.Text = "To: " + message.GetToWhoTheMassageIsSent();

            }

            return view;
        }

        private void SendOrRemove_Click(object sender, EventArgs e)
        {
            int pos = (int)sendOrRemove.Tag;
            DeleteMessage(pos);
        }


        public override AddMessageToFirebase this[int position]
        {
            get { return this.messageList[position]; }
        }

        public Messages Messages
        {
            get => default;
            set
            {
            }
        }
    }

    class order_AdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
} 
