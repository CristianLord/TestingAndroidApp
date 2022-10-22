using Android.App;
using Android.Content;
using Android.Net.Wifi.Aware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingAndroid
{
    [Activity(Label = "CalculatorActivity")]
    public class CalculatorActivity : Activity
    {
        public static readonly string KeyName = "Name";
        private bool CanAddOperation = false;
        private bool CanAddDecimal = true;
        private double ResultValue = 0.0d;
        private string Operation = string.Empty;

        //Views
        private TextView OperationView;
        private TextView ResultView;

        //Buttons - numbers
        private Button ZeroButton;
        private Button OneButton;
        private Button TwoButton;
        private Button ThreeButton;
        private Button FourdButton;
        private Button FiveButton;
        private Button SixButton;
        private Button SevenButton;
        private Button EightButton;
        private Button NineButton;
        private Button DotButton;

        //Buttons - operators
        private Button ClearButton;
        private Button BackButton;
        private Button EqualsButton;
        private Button PlusButton;
        private Button SubstractButton;
        private Button DivideButton;
        private Button MultiplyButton;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_calculator);

            TextView welcome = FindViewById<TextView>(Resource.Id.textName);
            OperationView = FindViewById<TextView>(Resource.Id.textNumber);
            ResultView = FindViewById<TextView>(Resource.Id.textResult);

            //Number buttons
            ZeroButton = FindViewById<Button>(Resource.Id.buttonZero);
            OneButton = FindViewById<Button>(Resource.Id.buttonOne);
            TwoButton = FindViewById<Button>(Resource.Id.buttonTwo);
            ThreeButton = FindViewById<Button>(Resource.Id.buttonThree);
            FourdButton = FindViewById<Button>(Resource.Id.buttonFourd);
            FiveButton = FindViewById<Button>(Resource.Id.buttonFive);
            SixButton = FindViewById<Button>(Resource.Id.buttonSix);
            SevenButton = FindViewById<Button>(Resource.Id.buttonSeven);
            EightButton = FindViewById<Button>(Resource.Id.buttonEight);
            NineButton = FindViewById<Button>(Resource.Id.buttonNine);
            DotButton = FindViewById<Button>(Resource.Id.buttonDot);

            //Operators buttons
            ClearButton = FindViewById<Button>(Resource.Id.buttonClear);
            BackButton = FindViewById<Button>(Resource.Id.buttonBack);
            EqualsButton = FindViewById<Button>(Resource.Id.buttonEquals);
            PlusButton = FindViewById<Button>(Resource.Id.buttonPlus);
            SubstractButton = FindViewById<Button>(Resource.Id.buttonSubstract);
            DivideButton = FindViewById<Button>(Resource.Id.buttonDivide);
            MultiplyButton = FindViewById<Button>(Resource.Id.buttonMultiply);

            //Click method to numbers
            OneButton.Click += Number_Click;
            TwoButton.Click += Number_Click;
            ThreeButton.Click += Number_Click;
            FourdButton.Click += Number_Click;
            FiveButton.Click += Number_Click;
            SixButton.Click += Number_Click;
            SevenButton.Click += Number_Click;
            EightButton.Click += Number_Click;
            ZeroButton.Click += Number_Click;
            NineButton.Click += Number_Click;
            DotButton.Click += Number_Click;

            //Click method to operators
            PlusButton.Click += Operation_Click;
            SubstractButton.Click += Operation_Click;
            MultiplyButton.Click += Operation_Click;
            DivideButton.Click += Operation_Click;
            ClearButton.Click += AllClear_Click;
            BackButton.Click += BackSpace_Click;
            EqualsButton.Click += EqualsButton_Click;

            var name = Intent.GetStringExtra(KeyName);
            welcome.Text = $"Hello, {name}";

        }

        /// <summary>
        /// Event when equals operator is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EqualsButton_Click(object sender, EventArgs e)
        {
            CalculateResult(Operation);
            OperationView.Text = ResultValue.ToString();
            ResultView.Text = string.Empty;
        }

        /// <summary>
        /// Clears the Operation view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllClear_Click(object sender, System.EventArgs e)
        {
            OperationView.Text = String.Empty;
            ResultView.Text = String.Empty;
            CanAddOperation = false;
            CanAddDecimal = true;
            ResultValue = 0.0d;
            Operation = string.Empty;
        }

        /// <summary>
        /// Deletes the last number entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackSpace_Click(object sender, System.EventArgs e)
        {
            var lenght = OperationView.Length();
            if(lenght > 0)
            {
                OperationView.Text = OperationView.Text.Substring(0, lenght - 1);
            }
        }

        /// <summary>
        /// Event when a number is entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Number_Click(object sender, System.EventArgs e)
        {
            if(sender is Button)
            {
                var button = sender as Button;
                if(button.Text.Equals("."))
                {
                    if(CanAddDecimal)
                    { 
                        OperationView.Append(button.Text);
                        CanAddDecimal = false;
                    }
                }
                else
                {
                    OperationView.Append(button.Text);
                }
                CanAddOperation = true;
            }
        }

        /// <summary>
        /// Event when a Operation is entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Operation_Click(object sender, System.EventArgs e)
        {
            if (sender is Button && CanAddOperation)
            {
                var button = sender as Button;
                if (!ResultView.Text.Equals(string.Empty))
                {
                    CalculateResult(Operation);
                    Operation = button.Text;
                    ResultView.Text = ResultValue.ToString() + Operation;
                }
                else
                {
                    ResultValue = double.Parse(OperationView.Text);
                    ResultView.Text = OperationView.Text;
                    Operation = button.Text;
                    ResultView.Append(Operation);
                }
                
                CanAddOperation = false;
                CanAddDecimal = true;
                OperationView.Text = string.Empty;
            }
        }

        /// <summary>
        /// Calculates the result.
        /// </summary>
        /// <param name="action">Operation to perfom.</param>
        private void CalculateResult(string action)
        {
            switch(action)
            {
                case "+":
                    ResultValue += double.Parse(OperationView.Text);
                    break;

                case "-":
                    ResultValue -= double.Parse(OperationView.Text);
                    break;

                case "x":
                    ResultValue *= double.Parse(OperationView.Text);
                    break;

                case "/":
                    ResultValue /= double.Parse(OperationView.Text);
                    break;

                default: break;
            }
        }
    }
}