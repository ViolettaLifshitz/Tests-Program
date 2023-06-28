using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests_Program
{
    public class AddMessageToFirebase
    {
        public string date { get; set; }
        public string toWhoTheMassageIsSent { get; set; }
        public string content { get; set; }
        public string userEmail { get; set; }
        public string messageId { get; set; }
        public string title { get; set; }

        FirebaseFirestore database;

        public const string MESSAGES_COLLECTION_NAME = "messages";
        public AddMessageToFirebase()
        {
            this.database = AppDataHelper.GetFirestore();
        }
        public AddMessageToFirebase(string toWhoTheMassageIsSent, string title, string userEmail, string content)
        {
            this.date = DateTime.Now.ToString("dd/MM/yyyy");
            this.toWhoTheMassageIsSent = toWhoTheMassageIsSent;
            this.userEmail = userEmail;
            this.content = content;
            this.title = title;
            this.database = AppDataHelper.GetFirestore();
        }
        public async Task<bool> NewMessage()
        {
            try
            {
                HashMap cartMap = new HashMap();
                cartMap.Put("date", this.date);
                cartMap.Put("email", this.userEmail);
                cartMap.Put("content", this.content);
                cartMap.Put("title", this.title);
                cartMap.Put("to", this.toWhoTheMassageIsSent);
                DocumentReference userReference = this.database.Collection(MESSAGES_COLLECTION_NAME).Document();
                cartMap.Put("Id", userReference.Id);

                this.messageId = userReference.Id;
                await userReference.Set(cartMap);

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return false;
            }
            return true;
        }
        public string GetTitle()
        {
            return this.title;
        }
        public string GetContent()
        {
            return this.content;
        }

        public string GetDate()
        {
            return this.date;
        }
        public string GetToWhoTheMassageIsSent()
        {
            return this.toWhoTheMassageIsSent;
        }
        public string GetEmail()
        {
            return this.userEmail;
        }
        public string GetMessageId()
        {
            return this.messageId;
        }
    }
}