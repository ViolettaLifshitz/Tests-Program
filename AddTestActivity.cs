using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests_Program
{
    [Activity(Label = "AddTestActivity")]
    public class AddTestActivity : Activity, ListView.IOnItemClickListener, ListView.IOnItemLongClickListener, View.IOnClickListener
    {
        Dialog d;
        EditText note;
        Button  date;
        ImageButton buttonBack, buttonAdd;
        public static Bitmap someTest;
        string email;
        Spinner title;
        string type;
        DatePickerDialog datePickerDialog;
        DateTime dateTime = DateTime.UtcNow.Date;
        string today = "";

        public AddTestToFirebase AddTestToFirebase
        {
            get => default;
            set
            {
            }
        }

        public Test Test
        {
            get => default;
            set
            {
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.single_test_layout);


            someTest = BitmapFactory.DecodeResource(base.Resources, Resource.Drawable.book);
            today = dateTime.ToString("dd.MM.yyyy");

            date = FindViewById<Button>(Resource.Id.testDate);
            note = FindViewById<EditText>(Resource.Id.note);
            buttonAdd = FindViewById<ImageButton>(Resource.Id.buttonAddOrRemove);
            title = FindViewById<Spinner>(Resource.Id.testTitle);
            buttonBack = FindViewById<ImageButton>(Resource.Id.btnBack);
            buttonBack.Click += ButtonBack_Click;
            buttonAdd.Click += ButtonAdd_Click;


            date.SetOnClickListener(this);
            title.ItemSelected += Title_ItemSelected;
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.title_array, Android.Resource.Layout.SimpleSpinnerDropDownItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            title.Adapter = adapter;

        }
    
        private async void ButtonAdd_Click(object sender, EventArgs e)
        {
            string anotherFormatDate = ProceedActivity.ExtractNumbersFromDate(date.Text);
            string[] arrayDateEntered = anotherFormatDate.Split('.');
            string[] arrayDateToday = today.Split('.');
            bool isValidDate = false;
            if (int.Parse(arrayDateEntered[2]) == int.Parse(arrayDateToday[2]))
            {
                if (int.Parse(arrayDateEntered[1]) == int.Parse(arrayDateToday[1]))
                {
                    if (int.Parse(arrayDateEntered[0]) >= int.Parse(arrayDateToday[0]))
                    {
                        isValidDate = true;
                    }
                }
                else if (int.Parse(arrayDateEntered[1]) > int.Parse(arrayDateToday[1]))
                {
                    isValidDate = true;
                }
            }
            else if (int.Parse(arrayDateEntered[2]) > int.Parse(arrayDateToday[2]))
            {
                isValidDate = true;
            }
            if (isValidDate)
            {
                Test test = new Test(type, someTest, date.Text, LoginActivity.emailText.Text, note.Text);
                //string title,Android.Graphics.Bitmap bitmap, DateTime utcNow, string email,string note
                AddTestToFirebase testAdd = new AddTestToFirebase(type, test.GetDate(), test.GetEmail(), note.Text);
                bool isInList = false;
                foreach (Test item in ProceedActivity.testsList)
                {
                    if (item.GetTitle().Equals(test.GetTitle()) && item.GetDate().Equals(test.GetDate()))
                    {
                        isInList = true;
                        Toast.MakeText(this, "Test is already added,please change test details", ToastLength.Long).Show();
                        break;
                    }
                }
                if (!isInList && await testAdd.NewTest())
                {
                    Toast.MakeText(this, "Test added succesfully", ToastLength.Long).Show();
                 //   ProceedActivity.testsList.Add(test);
                    if (Intent.GetStringExtra("source") != null && Intent.GetStringExtra("source") == "Tests")
                    {
                        this.Finish();
                    }
                    else
                    {
                        Intent intent = new Intent(this, typeof(AllTestsActivity));
                        StartActivity(intent);
                    }
                }
              
            }
            else
            {
                Toast.MakeText(this, "Please change to today's date or more late", ToastLength.Long).Show();

            }

        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        private void Title_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            type = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, type, ToastLength.Long).Show();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            throw new NotImplementedException();
        }

        public bool OnItemLongClick(AdapterView parent, View view, int position, long id)
        {
            throw new NotImplementedException();
        }

        public void OnClick(View v)
        {
            if (v == date)

            {

                DateTime today = DateTime.Today;



                datePickerDialog = new DatePickerDialog(this, OnDateSet, today.Year, today.Month - 1, today.Day);

                datePickerDialog.Show();

            }

        }
        void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)

        {

            String str = e.Date.ToLongDateString();

            Toast.MakeText(this, str, ToastLength.Long).Show();

            date.Text = str;

        }
  
    }
}