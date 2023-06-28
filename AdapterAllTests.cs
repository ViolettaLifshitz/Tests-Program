using Android.App;
using Android.Content;
using Android.Graphics;
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
    public class AdapterAllTests : BaseAdapter<Test>
    {
        List<Test> tests;
        Context context;
        ImageView cartImage;
        EditText  note;
        TextView textTestDate;
        TextView testTitle;
        ImageButton  btnRemove;

        public AdapterAllTests(Context context, List<Test> tests)
        {
            this.context = context;
            this.tests = tests;
        }
        public List<Test> GetList()
        {
            return this.tests;
        }
        public void AddCartToItem(Test test)
        {
            this.tests.Add(test);
            NotifyDataSetChanged();
        }
        public void DeleteTest(int position)
        {
            this.tests.RemoveAt(position);
            NotifyDataSetChanged();
        }
        public override Test this[int position]
        {
            get { return this.tests[position]; }
        }
        public override int Count
        {
            get { return this.tests.Count; }
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater layoutInflater;
            try
            {
                layoutInflater = ((AllTestsActivity)context).LayoutInflater;

            }
            catch
            {
                layoutInflater = ((AlarmAnimationActivity)context).LayoutInflater;
            }
            View view = layoutInflater.Inflate(Resource.Layout.single_test_layout, parent, false);
            // Button btnAddP = view.FindViewById<Button>(Resource.Id.btnAddP);
            //  Button btnRemove = view.FindViewById<Button>(Resource.Id.btnRemove);
            Spinner testTitleSpinner = view.FindViewById<Spinner>(Resource.Id.testTitle);
            testTitleSpinner.Visibility = ViewStates.Invisible;
            testTitle = view.FindViewById<TextView>(Resource.Id.textTestTitle);
            Button testDateButton = view.FindViewById<Button>(Resource.Id.testDate);
            testDateButton.Visibility = ViewStates.Invisible;
            ImageButton back = view.FindViewById<ImageButton>(Resource.Id.btnBack);
            back.Visibility = ViewStates.Invisible;
            note = view.FindViewById<EditText>(Resource.Id.note);
            textTestDate = view.FindViewById<TextView>(Resource.Id.textTestDate);
            btnRemove = view.FindViewById<ImageButton>(Resource.Id.buttonAddOrRemove);
            Test test = tests[position];
            btnRemove.Tag = position;
            btnRemove.SetImageBitmap(BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.deleteButton));
            btnRemove.Click += BtnRemove_Click;
            cartImage = new ImageView(context);
            if (test != null)
            {
                cartImage.SetImageBitmap(AddTestActivity.someTest);
                testTitle.Text = " " + test.GetTitle();
                textTestDate.Text = " " + test.GetDate();
                note.Text = " " + test.GetNote();
            }
            return view;
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            //Button btnDelSnd = (Button)sender;
            int pos = (int)btnRemove.Tag;
            DeleteTest(pos);


        }

        public Test Test
        {
            get => default;
            set
            {
            }
        }
    }

    class CartAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}