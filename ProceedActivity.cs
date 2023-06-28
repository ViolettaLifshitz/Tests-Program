using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Transitions;
using Android.Views;
using Android.Widget;
using Bumptech.Glide;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tests_Program
{
    [Activity(Label = "ProceedActivity")]
    public class ProceedActivity : Activity
    {
        Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>()
            {
                { "date",new List<string>(){ } },{"title",new List<string>(){ }}
                ,{"note",new List<string>(){ }}
            };
        public static List<Test> testsUrgent;
        DateTime dateTime = DateTime.UtcNow.Date;
        string today = "";
        
        public static Dictionary<string, string> months = new Dictionary<string, string>(){ { "January", "01" },{ "February","02"},
            { "March","03" },{"April","04" },{ "May","05" }, {"June","06" },
            { "July","07" }, {"August","08" }, {"September","09" }, {"October","10" },{ "November","11" },
            { "December","12" } };
        public static List<Test> testsList;
        public static AdapterAllTests testsAdapter;
        public static List<AddMessageToFirebase> messagesFromFirebase;
        public static List<AddMessageToFirebase> messagesToShow;
        public static List<Messages> messageList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.proceed_layout);
            today = dateTime.ToString("dd.MM.yyyy");
            ImageButton gifImageView = FindViewById<ImageButton>(Resource.Id.gifImageView);
            Glide.With(this)
                     .AsGif()
                     .Load(Resource.Drawable.
                     cat) // Replace with the name of your GIF file
                     .Into(gifImageView);
            Thread.Sleep(8000);
            testsList = new List<Test>();
            testsUrgent = new List<Test>();
            messagesFromFirebase = new List<AddMessageToFirebase>();
            messagesToShow = new List<AddMessageToFirebase>();
            messageList = new List<Messages>();
            ListenerTests testsListener = new ListenerTests();
            testsListener.OnTestsRetrieved += TestsListener_OnTestsRetrieved;
        }

    
        public static string ExtractNumbersFromDate(string date)
        {
            int countForFirstNumber = 0;
            int countForDot = 0;
            string extract = "";
            for(int i = 0; i < date.Length; i++)
            {
                if (countForFirstNumber == 0)
                {
                    if (date[i] >= '0' && date[i] <= '9')
                    {
                        extract += date[i];
                        countForFirstNumber++;
                    }
                }
                else
                {
                    if(date[i]==' ')
                    {

                            extract += '.';
                        countForDot++;
                    }
                    else
                    {
                        if (date[i] >= '0' && date[i] <= '9')
                        {
                            extract +=  date[i];
                        }
                    }
                }

            }
            int index = (extract.Length / 4)+1 ;
            string monthToInsert = "";
            foreach(string key in months.Keys)
            {
                if (date.Contains(key))
                {
                    monthToInsert = months[key];
                    break;
                }
            }
            if (countForDot==1)
            {
                extract=extract.Insert(index, monthToInsert);
                return extract.Insert(index, ".");
                
            } 
            return extract.Insert(index,monthToInsert);
        }
        private void TestsListener_OnTestsRetrieved(object sender, ListenerTests.TestsEventArgs e)
        {
            foreach (var item in e.tests)
            {
                if (item.GetEmail() == LoginActivity.emailText.Text)
                {
                    if (dict["date"].Count != 0)
                    {
                        if (!(dict["date"].Contains(item.GetDate()) && dict["title"].Contains(item.GetTitle()) && dict["note"].Contains(item.GetNote())))
                        {
                            testsList.Add(new Test(item.GetTitle(), AddTestActivity.someTest, item.GetDate(), item.GetEmail(), item.GetNote()));                    //"gfdgsdfgfdgs"  "dfsdfs"
                        }
                    }
                    else
                    {
                        testsList.Add(new Test(item.GetTitle(), AddTestActivity.someTest, item.GetDate(), item.GetEmail(), item.GetNote()));                    //"gfdgsdfgfdgs"  "dfsdfs"
                    }
                    Test testUrgent = new Test(item.GetTitle(), AddTestActivity.someTest, item.GetDate(), item.GetEmail(), item.GetNote());
                    string anotherFormatDate = ExtractNumbersFromDate(item.GetDate());
                    string[] arrayDateEntered = anotherFormatDate.Split('.');
                    string[] arrayDateToday = today.Split('.');
                    bool isTestInThisMonth=false;
          
                        if (int.Parse(arrayDateEntered[1]) == int.Parse(arrayDateToday[1]))
                        {
                            isTestInThisMonth = true;
                        }
          
                    if (!testsUrgent.Contains(testUrgent) && isTestInThisMonth)
                    {
                        testsUrgent.Add(testUrgent);
                    }
                    dict["date"].Add(item.GetDate());           //"dfsdfs"
                    dict["title"].Add(item.GetTitle());
                    dict["note"].Add(item.GetNote());
                }
            }
                if (testsUrgent.Count != 0)
                {
                    Intent intent = new Intent(this, typeof(AlarmAnimationActivity));
                    StartActivity(intent);
                }
                else
                {
                    Intent intent = new Intent(this, typeof(MenuActivity));
                    StartActivity(intent);
                }
            
        }
    }
}