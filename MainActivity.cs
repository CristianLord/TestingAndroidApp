using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System.Linq;

namespace TestingAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        EditText name = null;
        Button buttonNext = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            name = FindViewById<EditText>(Resource.Id.textName);
            buttonNext = FindViewById<Button>(Resource.Id.buttonNext);

            name.TextChanged += Name_TextChanged;

            buttonNext.Click += ButtonNext_Click;
        }

        //Validate if the name is not empty or short.
        private void Name_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (e.Text.Count() >= 3)
            {
                buttonNext.Enabled = true;
            }
            else
            {
                buttonNext.Enabled = false;
            }
        }

        //Button next click method
        private void ButtonNext_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(CalculatorActivity));
            intent.PutExtra(CalculatorActivity.KeyName, name.Text);
            StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}