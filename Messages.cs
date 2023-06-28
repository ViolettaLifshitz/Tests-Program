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
    public class Messages
    {
        public string toWhoTheMassageIsSent;
        public string userEmail;
        public string date;
        public string content;
        public string title;

        public string GetDate()
        {
            return this.date;
        }
        public string GetEmail()
        {
            return this.userEmail;
        }
        public string GetTitle()
        {
            return this.title;
        }
        public string GetToWhoTheMessageIsSent()
        {
            return this.toWhoTheMassageIsSent;
        }


        public Messages(string toWhoTheMassageIsSent, string title, string userEmail, string content)
        {
            this.date = DateTime.Now.ToString("dd/MM/yyyy"); ;
            this.title = title;
            this.userEmail = userEmail;

            this.toWhoTheMassageIsSent = toWhoTheMassageIsSent;
            this.content = content;

        }

        public string GetContent()
        {
            return this.content;
        }

    }
}