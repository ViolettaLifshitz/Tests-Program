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
using System.Text.RegularExpressions;

namespace Tests_Program
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        EditText emailD, passwordD, phoneNumberD, nameD;
        Button buttonRegister, buttonLogin, buttonBack;
        RadioGroup type;
        string typeUser = "student";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.register_layout);
            // Create your application here
            emailD = FindViewById<EditText>(Resource.Id.email);
            passwordD = FindViewById<EditText>(Resource.Id.password);
            phoneNumberD = FindViewById<EditText>(Resource.Id.phoneNumberD);
            nameD = FindViewById<EditText>(Resource.Id.nameD);
            buttonRegister = FindViewById<Button>(Resource.Id.btnRegister);
            buttonRegister.Click += ButtonRegister_Click;
            buttonLogin = FindViewById<Button>(Resource.Id.btnLogin);
            buttonLogin.Click += ButtonLogin_Click;
            type = FindViewById<RadioGroup>(Resource.Id.type);
            type.CheckedChange += Type_CheckedChange;
            buttonBack = FindViewById<Button>(Resource.Id.btnBack);
            buttonBack.Click += ButtonBack_Click;
            // Create your application here
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            if (Intent.GetStringExtra("source") != null && Intent.GetStringExtra("source") == "Login")
            {
                this.Finish();
            }
            else
            {
                Intent intent = new Intent(this, typeof(LoginActivity));
                intent.PutExtra("source", "Register");
                StartActivity(intent);
            }
        }
        private static bool IsValidEmail(string email)
        {
            string regex = @"^[a-zA-Z0-9]+@[a-zA-Z]+\.[a-zA-Z]{2,4}$";
            //string regex = @"^[a - zA - Z0 - 9_.+ -] +(@gmail)+\.(com|net|org|gov)$";
            //a-zA - Z0 - 9 -
            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
        static bool IsValidName(string name)
        {
            // Regular expression pattern for name validation
            string pattern = @"^[a-zA-Z ]+$";

            // Create a Regex object with the pattern
            Regex regex = new Regex(pattern);

            // Use the Matches method to check if the name matches the pattern
            Match match = regex.Match(name);

            // Return true if the name matches the pattern, otherwise false
            return match.Success;
        }
        private async void ButtonRegister_Click(object sender, EventArgs e)
        {
            if (emailD.Text == "" || passwordD.Text == "" || phoneNumberD.Text == "" || nameD.Text == "")
            {
                Toast.MakeText(this, "all fildes are required, please enter them all", ToastLength.Long).Show();
                return;
            }
            if (nameD.Text.Length < 6)
            {
                Toast.MakeText(this, "name length must at least 6", ToastLength.Long).Show();
                return;

            }
            if (!IsValidName(nameD.Text))
            {
                Toast.MakeText(this, "name should contain only letters", ToastLength.Long).Show();
                return;
            }
            if (!IsValidEmail(emailD.Text))
            {
                Toast.MakeText(this, "invalid characters in email,email should contain one @ and one com|net|org|gov", ToastLength.Long).Show();
                return;
            }
            if (passwordD.Text.Length < 6)
            {
                Toast.MakeText(this, "password length must at least 6", ToastLength.Long).Show();
                return;

            }
            else
            {
                bool number = false;
                for (int i = 0; i < passwordD.Text.Length && number == false; i++)
                {
                    if (passwordD.Text[i] >= 'a' && passwordD.Text[i] <= 'z' || passwordD.Text[i] >= 'A' && passwordD.Text[i] <= 'Z')
                    {
                        number = false;

                    }
                    else
                    {
                        number = true;
                    }
                }
                if (number == false)
                {
                    Toast.MakeText(this, "password must contain at least one number", ToastLength.Long).Show();
                    return;
                }
            }
     
            if (phoneNumberD.Text.Length < 10)
            {
                Toast.MakeText(this, "invalid phone number", ToastLength.Long).Show();
                return;
            }
            else
            {
                for (int i = 0; i < phoneNumberD.Text.Length; i++)
                {
                    if (phoneNumberD.Text[i] < '0' || phoneNumberD.Text[i] > '9')
                    {

                        Toast.MakeText(this, "invalid phone number", ToastLength.Long).Show();
                        return;
                    }
                }
                try
                {
                    User user = new User(nameD.Text, emailD.Text, passwordD.Text, phoneNumberD.Text, typeUser);

                    if (await user.Register() == true)
                    {
                        Toast.MakeText(this, "you registerd successfully!", ToastLength.Long).Show();
                        Intent intent = new Intent(this, typeof(MainActivity));
                        StartActivity(intent);
                    }
                    else
                    {
                        //you enterd invalid details. please try again!
                        Toast.MakeText(this, ""+User.s, ToastLength.Long).Show();
                    }
                }
                catch (Exception ex)
                {//Error register
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
            }
        }

        private void Type_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            if (e.CheckedId == Resource.Id.typeStudent)
            {
                typeUser = "student";
            }
            else if (e.CheckedId == Resource.Id.typeTeacher)
            {
                typeUser = "teacher";
            }
        }


    }
}