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
using System.Threading;
using System.Threading.Tasks;

namespace Tests_Program
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity, Android.Views.View.IOnClickListener
    {

        public static EditText emailText, passwordText;
        Button loginButton, registerButton, backButton;
        ISharedPreferences SP;
        string mail;
        public static bool isLogged = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.login_layout);
            emailText = FindViewById<EditText>(Resource.Id.email);
            passwordText = FindViewById<EditText>(Resource.Id.password);
            loginButton = FindViewById<Button>(Resource.Id.btnLogin);
            SP = this.GetSharedPreferences(User.CURRENT_USER_FILE, FileCreationMode.Private);
            mail = SP.GetString("email", "");
            string pass = SP.GetString("password", "");
            if (mail != "" && pass != "")
            {
                emailText.Text = mail;
                passwordText.Text = pass;
            }
            registerButton = FindViewById<Button>(Resource.Id.btnRegister);
            backButton = FindViewById<Button>(Resource.Id.btnBack);
            loginButton.SetOnClickListener(this);
            backButton.SetOnClickListener(this);
            registerButton.SetOnClickListener(this);

        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        public async void LoginButton()
        {
            if (emailText.Text == "" && passwordText.Text == "")
            {
                Toast.MakeText(this, "plese enter email and password", ToastLength.Long).Show();
                return;
            }
            if (passwordText.Text.Length < 6)
            {
                Toast.MakeText(this, "password length must at least 6", ToastLength.Long).Show();
                return;

            }
            else
            {
                bool number = false;
                for (int i = 0; i < passwordText.Text.Length && number == false; i++)
                {
                    if (passwordText.Text[i] >= 'a' && passwordText.Text[i] <= 'z' || passwordText.Text[i] >= 'A' && passwordText.Text[i] <= 'Z')
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
            try
            {
                User user = new User(emailText.Text, passwordText.Text);


                if (await user.Login() == true)
                {
                    Toast.MakeText(this, "youre logged successfully!", ToastLength.Long).Show();
                    isLogged = true;
       
                        Intent intent = new Intent(this, typeof(ProceedActivity));
                        StartActivity(intent);
                    
                }
                else
                {
                    Toast.MakeText(this, "wrong email or password, try again!", ToastLength.Long).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Error login", ToastLength.Long).Show();
            }
        }
        public void OnClick(View v)
        {
            if (v == loginButton)
            {
                LoginButton();
            }
            else if(v== backButton)
            {
                this.Finish();
            }
            else if (v == registerButton)
            {
                if (Intent.GetStringExtra("source") != null && Intent.GetStringExtra("source") == "Register")
                {
                    this.Finish();
                }
                else
                {
                    Intent intent = new Intent(this, typeof(RegisterActivity));
                    intent.PutExtra("source", "Login");
                    StartActivity(intent);
                }
            }
        }
           
    }
}