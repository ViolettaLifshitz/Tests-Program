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
using Firebase.Auth;
using Firebase.Firestore;
using Java.Util;
using System.Threading.Tasks;
using Android.Gms.Extensions;
using Android.Gms.Common.Apis;

namespace Tests_Program
{
    public class User
    {
        // ברגע שמשתמש מתחבר אוטמטית שומרים את הנתונים בsharedprafernce
        string name { get; set; }
        string email { get; set; }
        string password { get; set; }
        string phoneNumber { get; set; }
        public static string s;
        string typeUser { get; set; }
        FirebaseAuth firebaseAthentication;//משתנה שיצביע לטבלה באוטנטיקשיין
        FirebaseFirestore database;
        public const string COLLECTION_NAME = "users";//איך שאנחנו קוראים לטבלה בFIRESTORE
        public const string CURRENT_USER_FILE = "currentUserFile";//שם של הטבלה בSHARED PREFERENSE
        public string GetTypeUser()
        {
            return this.typeUser;
        }

        public string GetName()
        {
            return this.name;
        }
        public string GetEmail()
        {
            return this.email;
        }
        public string GetPassword()
        {
            return this.password;
        }
        public string GetPhoneNumber()
        {
            return this.phoneNumber;
        }
        public User()
        {
            this.firebaseAthentication = AppDataHelper.GetFirebaseAuthentication();
            this.database = AppDataHelper.GetFirestore();
        }
        public User(string Email, string Password)
        {
            this.name = " ";
            this.email = Email;
            this.password = Password;
            this.firebaseAthentication = AppDataHelper.GetFirebaseAuthentication();
            this.database = AppDataHelper.GetFirestore();
        }
        public User(string Name, string Email, string Password, string phoneNumber, string typeUser)
        {
            this.name = Name;
            this.email = Email;
            this.password = Password;
            this.phoneNumber = phoneNumber;
            this.typeUser = typeUser;
            this.firebaseAthentication = AppDataHelper.GetFirebaseAuthentication();
            this.database = AppDataHelper.GetFirestore();

        }

        //רק כשהמשתמש מתחבר שומרים את הנתונים בsharparfernce.
        // הנתונים בshareprafernce נשמרים רק במכשיר רי אפשר לפנות לנתונים האלה ממכשיר אחר.
        public async Task<bool> Login()
        {
            try
            {
                //כשמבצעיים הרצה אפשר לבצע הרצה של כמה דברים במקביל כדי לבצע הרצה של כמה דברים במקביל אנחנו משתמשים בASYNC
                //כאשר אנו משתמשים בFIRESTORE לוקח זמן עד שהוא מגיב כי הרבה אנשים בעולם משתמשים בו
                //אז מה שאנחנו עושים זה לומר לאפליקציה שתמשיך לחכות וגם תעשה משהו במקביל כדי לא לתקוע את האפליקציה מפני שיכול לקרות מצב
                //שהאפליקציה לא תגיב וגם המקלדת שום דבר לא יגיב על ידי שימוש באסינכורניות אנחנו יכולים לעשות את הגלגל שמסתובב
                //כל פעולה שאנחנו עובדים עם DATA BASE חייב לעשות אסינכרונית כדי שהאפליקציה לא תתקע והתכנית לא תחשוב שמשהו לא בסדר
                await this.firebaseAthentication.SignInWithEmailAndPassword(this.email, this.password);
                var editor = Application.Context.GetSharedPreferences(CURRENT_USER_FILE, FileCreationMode.Private).Edit();//באמצעות VAR הקומפיילר יכול להתאים בעצמו את טיפוס המשתנה מבלי שכתבנו את טיפוס המשתנה
                editor.PutString("email", this.email);
                editor.PutString("password", this.password);
                editor.Apply();// הפקודה מעדכנת
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return false;
            }
            return true;
        }
        public void SetName(string name)
        {
            this.name = name;
        }
        public void SetPhone(string phone)
        {
            this.phoneNumber = phone;
        }
        public void SetPassword(string password)
        {
            this.password = password;
        }
        public void SetEmail(string email)
        {
            this.email = email;
        }
        public void SetType(string type)
        {
            this.typeUser = type;
        }
        public async Task<bool> Register()
        {
            
            try
            {
                //Product p = null;
                //synchronized(lock)
                //{
                //    while (q.peek() == null)
                //    {
                //        lock.wait();
                //    }
                //    p = q.remove();
                //}
                await this.firebaseAthentication.CreateUserWithEmailAndPassword(this.email, this.password);

            }
            catch (Exception ex)
            {
                s = ex.Message;
                return false;
            }
            try
            {
                HashMap userMap = new HashMap();
                userMap.Put("email", this.email);
                userMap.Put("fullName", this.name);
                userMap.Put("phonNumber", this.phoneNumber);
                userMap.Put("typeUser", this.typeUser);
                DocumentReference userReference = this.database.Collection(COLLECTION_NAME).Document(this.firebaseAthentication.CurrentUser.Uid); // תלך לטבלה ותדבוק אם קיימת  ואם לא תיצור 
                await userReference.Set(userMap);// קח את המשתנה ותשים את המשתנה
            }
            catch (Exception ex)
            {
                return false;

            }
            return true;
        }

        public async Task<bool> Logout()
        {
            try
            {
                var editor = Application.Context.GetSharedPreferences(User.CURRENT_USER_FILE, FileCreationMode.Private).Edit();
                editor.PutString("email", "");
                editor.PutString("password", "");
                editor.Apply(); // התנתקות בפיירבייס חשוב!
                firebaseAthentication.SignOut();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}