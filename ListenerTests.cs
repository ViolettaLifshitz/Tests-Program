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
    public class ListenerTests : Java.Lang.Object, IEventListener
    {
        List<AddTestToFirebase> testsList = new List<AddTestToFirebase>();//הגדרת רשימה של פריטים בהתאם לקולקשיין
        public event EventHandler<TestsEventArgs> OnTestsRetrieved;//אירוע שאליו ירשמו כל מי שמעוניין לדעת שהיה שינוי באוסף קטגוריות
        public class TestsEventArgs//האיוונט ארג הוא אחד מהפרמטרים של כל פעולה כשעושים אירוע קליק
: EventArgs
        {
            //internal-ספציפי מוגדר באסמבלי שונה למשל 
            //What is the assembly in C#?
            //An assembly is a collection of types and resources that are built to work together and form a logical 
            //    unit of functionality.Assemblies take the form of executable(.exe) or dynamic link library(. dll) files,
            //    and are the building blocks of.NET applications
            internal List<AddTestToFirebase> tests { get; set; }
        }
        public IntPtr Handle;

        public ListenerTests()//פעולה בונה מבקשת שיאזין לאוסף ההזמנות
        {
            AppDataHelper.GetFirestore().Collection(AddTestToFirebase.TESTS_COLLECTION_NAME).AddSnapshotListener(this);
        }
        public void OnEvent(Java.Lang.Object value, FirebaseFirestoreException error)
        {
            var snaoshot = (QuerySnapshot)value;
            this.testsList = new List<AddTestToFirebase>();
            foreach (DocumentSnapshot item in snaoshot.Documents)
            {
                AddTestToFirebase test = new AddTestToFirebase();

                if (item.Get("title") != null)
                {
                    test.SetTitle(item.Get("title").ToString());
                }
                else
                {
                    test.SetTitle("");
                }
                if (item.Get("email") != null)
                {
                    test.SetEmail(item.Get("email").ToString());
                }
                else
                {
                    test.SetEmail("");
                }
                if (item.Get("email") != null)
                {
                    test.SetEmail(item.Get("email").ToString());
                }
                else
                {
                    test.SetEmail("");
                }
                if (item.Get("testDate") != null)
                {
                    test.SetDate(item.Get("testDate").ToString());
                }
                else
                {
                    test.SetDate("");
                }
                if (item.Get("note") != null)
                {
                    test.SetNote(item.Get("note").ToString());
                }
                else
                {
                    test.SetNote("");
                }
                this.testsList.Add(test);
            }
                if (this.OnTestsRetrieved != null)
                {
                    TestsEventArgs e = new TestsEventArgs();
                    e.tests = this.testsList;
                    //זימון של כל הפעולות שנרשמו לאירוע
                    OnTestsRetrieved.Invoke(this, e);//שומר את כל ההמקומות שהיינו בהם ברשימה
                    //כאשר אנו עושים אירוע בכפתור או איפשהו אנו נרשמים להאזנה של ההאירוע
                }
            
        }

        public Test Test
        {
            get => default;
            set
            {
            }
        }
    }
}