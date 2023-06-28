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
    public class AddTestToFirebase
    {
        private string testId { get; set; }
        private string title { get; set; }
        private string email { get; set; }
        private string testDate { get; set; }
        private string note { get; set; }

        FirebaseFirestore database;

        public const string TESTS_COLLECTION_NAME = "tests";
        public AddTestToFirebase()
        {
            this.database = AppDataHelper.GetFirestore();
        }
        public AddTestToFirebase(string title, string testDate, string email, string note)
        {
            this.title = title;
            this.testDate = testDate;
            this.email = email;
            this.note = note;
            this.database = AppDataHelper.GetFirestore();
        }
        public async Task<bool> NewTest()
        {
            try
            {
                HashMap cartMap = new HashMap();
                cartMap.Put("title", this.title);
                cartMap.Put("email", this.email);
                cartMap.Put("testDate", this.testDate.ToString());
                cartMap.Put("note", this.note);
                DocumentReference userReference = this.database.Collection(TESTS_COLLECTION_NAME).Document();
                cartMap.Put("Id", userReference.Id);

                this.testId = userReference.Id;
                await userReference.Set(cartMap);

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return false;
            }
            return true;
        }
        public void SetTitle(string title)
        {
            this.title = title;
        }
        public void SetDate(string date)
        {
            this.testDate = date;
        }
        public void SetEmail(string email)
        {
            this.email = email;
        }
        public void SetNote(string note)
        {
            this.note = note;
        }
        public string GetTitle()
        {
            return this.title;
        }
        public string GetDate()
        {
            return this.testDate;
        }
        public string GetEmail()
        {
            return this.email;
        }

        public string GetTestId()
        {
            return this.testId;
        }
        public string GetNote()
        {
            return this.note;
        }
    }
}