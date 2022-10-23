using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {

        int cs = 1;
        double N1,N2;
        string myoperator;
        public MainPage()
        {
            InitializeComponent();
            OnClear(this, null);
        }

        public static class OperatorHelper
        {
            public static double Calculate(double value1, double value2, string myoperator)
            {
                double ans = 0;
                switch (myoperator)
                {
                    case "÷":
                        ans = value1 / value2;
                        break;
                    case "*":
                        ans = value1 * value2;
                        break;
                    case "+":
                        ans = value1 + value2;
                        break;
                    case "-":
                        ans = value1 - value2;
                        break;
                                   }
                return ans;
            }
        }
        private void OnClear(object sender, EventArgs e)
        { 
                N1 = 0;
                N2 = 0;
                cs = 1;
                this.output.Text = "0";
            
        }

        private void OnSquareRoot(object sender, EventArgs e)
        {
            if ((cs == -1) || (cs == 1))
            {
                var result = Math.Sqrt(N1);
                this.output.Text = result.ToString();
                N1 = result;
                cs = -1;
            }
        }

        private void OnPercentage(object sender, EventArgs e)
        {
            if ((cs == -1) || ( cs == 1))//please use some break point to check when we are going to get current state value as -1 or 1
            {                
                var result = N1 / 100;
                this.output.Text = result.ToString();
                N1 = result;
                cs = -1;

            }
        }

        private void OnSelectNumber(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            string buttonPressedText = button.Text;
            //validation before button is pressed

            //if the current result is 0 in text box then we will direct the calculator to exclude 0 when pressing busttons
            if (this.output.Text == "0" || cs < 0)//at first current state is 1
            {
                this.output.Text = "";//here the text value will be cleared when pressing button

                if (cs < 0) //at first current value is 1 so this condition is excluded
                    cs *= -1;
            }

            this.output.Text += buttonPressedText;// this condition is called when current state is greater and text box will aquire the pressed 

            double number;//if  we are going  to assign two dynamic number for a given variable using try parse method 
            if (double.TryParse(this.output.Text, out number))
            {
                this.output.Text = number.ToString("N0");
                if (cs == 1)
                {
                    N1 = number;//at first current state will be 1 and it will assign first number with the pressed number variable
                }
                else
                {
                    N2 = number;//it will be implemented as the number of current state changes i.e. 2
                }
            }
        }

     

        private void OnCalculate(object sender, EventArgs e)
        {
            if (cs == 2)
            {
                var result = OperatorHelper.Calculate(N1, N2, myoperator);

                this.output.Text = result.ToString();
                N1 = result;
                cs = -1;
            }
        }

        private void OnSelectOperator(object sender, EventArgs e)
        {
            cs = -2;
            Button button = (Button)sender;
            string buttonPressedText = button.Text;
            myoperator = buttonPressedText;
        }
    }
}
