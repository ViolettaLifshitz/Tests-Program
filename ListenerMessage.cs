using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests_Program
{
    public class ListenerMessage : Java.Lang.Object, IEventListener
    {
        List<AddMessageToFirebase> messagesList = new List<AddMessageToFirebase>();//הגדרת רשימה של פריטים בהתאם לקולקשיין
        public event EventHandler<MessageEventArgs> OnMessageRetrieved;//אירוע שאליו ירשמו כל מי שמעוניין לדעת שהיה שינוי באוסף קטגוריות
        public class MessageEventArgs//האיוונט ארג הוא אחד מהפרמטרים של כל פעולה כשעושים אירוע קליק
: EventArgs
        {
            //internal-ספציפי מוגדר באסמבלי שונה למשל 
            //What is the assembly in C#?
            //An assembly is a collection of types and resources that are built to work together and form a logical 
            //    unit of functionality.Assemblies take the form of executable(.exe) or dynamic link library(. dll) files,
            //    and are the building blocks of.NET applications
            internal List<AddMessageToFirebase> messages { get; set; }
        }
        public IntPtr Handle;

        public ListenerMessage()//פעולה בונה מבקשת שיאזין לאוסף ההזמנות
        {
            AppDataHelper.GetFirestore().Collection(AddMessageToFirebase.MESSAGES_COLLECTION_NAME).AddSnapshotListener(this);
        }
        public void OnEvent(Java.Lang.Object value, FirebaseFirestoreException error)
        {
            var snaoshot = (QuerySnapshot)value;
            this.messagesList = new List<AddMessageToFirebase>();
            foreach (DocumentSnapshot item in snaoshot.Documents)
            {
                AddMessageToFirebase test = new AddMessageToFirebase();

                if (item.Get("date") != null)
                {
                    test.date = item.Get("date").ToString();
                }
                else
                {
                    test.date = "";
                }
                if (item.Get("email") != null)
                {
                    test.userEmail = item.Get("email").ToString();
                }
                else
                {
                    test.userEmail = "";
                }
                if (item.Get("Id") != null)
                {
                    test.messageId = item.Get("Id").ToString();
                }
                else
                {
                    test.messageId = "";
                }
                if (item.Get("content") != null)
                {
                    test.content = item.Get("content").ToString();
                }
                else
                {
                    test.content = "";
                }
                if (item.Get("title") != null)
                {
                    test.title = item.Get("title").ToString();
                }
                else
                {
                    test.title = "";
                }
                if (item.Get("to") != null)
                {
                    test.toWhoTheMassageIsSent = item.Get("to").ToString();
                }
                else
                {
                    test.toWhoTheMassageIsSent = "";
                }
                this.messagesList.Add(test);
            }
                if (this.OnMessageRetrieved != null)
                {
                    MessageEventArgs e = new MessageEventArgs();
                    e.messages = this.messagesList;
                    //זימון של כל הפעולות שנרשמו לאירוע
                    OnMessageRetrieved.Invoke(this, e);//שומר את כל ההמקומות שהיינו בהם ברשימה
                    //כאשר אנו עושים אירוע בכפתור או איפשהו אנו נרשמים להאזנה של ההאירוע
                }
            
        }


    }
}