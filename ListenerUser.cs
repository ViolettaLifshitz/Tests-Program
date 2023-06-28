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
    public class ListenerUser : Java.Lang.Object, IEventListener
    {
        List<User> usersList = new List<User>();//הגדרת רשימה של פריטים בהתאם לקולקשיין
        public event EventHandler<UsersEventArgs> OnUsersRetrieved;//אירוע שאליו ירשמו כל מי שמעוניין לדעת שהיה שינוי באוסף קטגוריות
        public class UsersEventArgs//האיוונט ארג הוא אחד מהפרמטרים של כל פעולה כשעושים אירוע קליק
: EventArgs
        {
            //internal-ספציפי מוגדר באסמבלי שונה למשל 
            //What is the assembly in C#?
            //An assembly is a collection of types and resources that are built to work together and form a logical 
            //    unit of functionality.Assemblies take the form of executable(.exe) or dynamic link library(. dll) files,
            //    and are the building blocks of.NET applications
            internal List<User> users { get; set; }
        }
        public IntPtr Handle;

        public ListenerUser()//פעולה בונה מבקשת שיאזין לאוסף ההזמנות
        {
            AppDataHelper.GetFirestore().Collection(User.COLLECTION_NAME).AddSnapshotListener(this);
        }
        public void OnEvent(Java.Lang.Object value, FirebaseFirestoreException error)
        {
            var snaoshot = (QuerySnapshot)value;
            this.usersList = new List<User>();
            foreach (DocumentSnapshot item in snaoshot.Documents)
            {
                User user = new User();

                if (item.Get("fullName") != null)
                {
                    user.SetName(item.Get("fullName").ToString());
                }
                else
                {
                    user.SetName("");
                }
                if (item.Get("phonNumber") != null)
                {
                    user.SetPhone(item.Get("phonNumber").ToString());
                }
                else
                {
                    user.SetPhone("");
                }
                if (item.Get("email") != null)
                {
                    user.SetEmail(item.Get("email").ToString());
                }
                else
                {
                    user.SetEmail("");
                }
                if (item.Get("typeUser") != null)
                {
                    user.SetType(item.Get("typeUser").ToString());
                }
                else
                {
                    user.SetType("");
                }
                this.usersList.Add(user);
            }
                if (this.OnUsersRetrieved != null)
                {
                    UsersEventArgs e = new UsersEventArgs();
                    e.users = this.usersList;
                    //זימון של כל הפעולות שנרשמו לאירוע
                    OnUsersRetrieved.Invoke(this, e);//שומר את כל ההמקומות שהיינו בהם ברשימה
                    //כאשר אנו עושים אירוע בכפתור או איפשהו אנו נרשמים להאזנה של ההאירוע
                }
           
        }

        public User User
        {
            get => default;
            set
            {
            }
        }
    }
}