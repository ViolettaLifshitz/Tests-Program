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

namespace Tests_Program
{
    public class Test
    {
        private string note;
        private string email;
        private string date;
        private string title;
        private Android.Graphics.Bitmap bitmap;
        public Test(string title, Android.Graphics.Bitmap bitmap, string utcNow, string email, string note)  
        {//  TestDetails orders = new TestDetails(90, DateTime.UtcNow, "ewer@gmail.com")
            this.date = utcNow;
            this.email = email;
            this.note = note;
            this.title = title;
            this.bitmap = bitmap;
        }
        public Android.Graphics.Bitmap GetBitmap()
        {
            return this.bitmap;
        }
        public void SetTitle(string title)
        {
            this.title = title;
        }
        public string GetTitle()
        {
            return this.title;
        }
        public string GetDate()
        {
            return this.date;
        }
        public string GetEmail()
        {
            return this.email;
        }
        public void SetDate(string date)
        {
            this.date = date;
        }
        public Test(string date, string title, Android.Graphics.Bitmap bitmap, string note) 
        {
            this.date = date;
            this.title = title;
            this.note = note;
            this.bitmap = bitmap;
        }

        public string GetNote()
        {
            return this.note;
        }
        public void SetNote(string note)
        {
            this.note = note;
        }
    }
}