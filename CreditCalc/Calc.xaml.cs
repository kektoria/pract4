using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CreditCalc
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calc : ContentPage
    {
        public Calc()
        {
            InitializeComponent();
        }

        private void SliderValueChange(object sender, ValueChangedEventArgs e)
        {
            SliderLabel.Text = $"{Slider.Value}%";

            if (LoanEntry.Text != "" && MonthEntry.Text != "")
            {
                Calculation(LoanEntry.Text, MonthEntry.Text, PaymentTypePicker.SelectedIndex, Slider.Value);
            }
            else
            {
                MonthlyPaymentLabel.Text = "Ежемесячный платеж: ...";
                TotalLabel.Text = "Общая сумма: ...";
                OverpaymentLabel.Text = "Переплата: ...";
            }
        }

        private void Calculation(string EntryLoanAmount, string EntryTermMonth, int PickerPayment, double Slider)
        {
            if (Convert.ToDouble(EntryTermMonth) != 0 && Convert.ToDouble(EntryLoanAmount) != 0)
            {
                switch (PickerPayment)
                {
                    case 0:
                        {
                            double EveryMonthPay = (Convert.ToDouble(EntryLoanAmount) + Convert.ToDouble(EntryLoanAmount) * Slider / 100) / Convert.ToDouble(EntryTermMonth);

                            MonthlyPaymentLabel.Text = $"Ежемесячный платеж: {Math.Round(((Convert.ToDouble(EntryLoanAmount) + Convert.ToDouble(EntryLoanAmount) * Slider / 100) / Convert.ToDouble(EntryTermMonth)), 2)}";
                            TotalLabel.Text = $"Общая сумма: {Math.Round(EveryMonthPay * Convert.ToDouble(EntryTermMonth), 2)}";
                            OverpaymentLabel.Text = $"Переплата: {Math.Round((Convert.ToDouble(EntryTermMonth) - Convert.ToDouble(EntryLoanAmount)), 2)}";
                        }
                        break;
                    case 1:
                        {
                            double EveryMonthPay = Convert.ToDouble(EntryLoanAmount) * (Slider + (Slider / (Math.Pow((1 + Slider), (Convert.ToDouble(EntryTermMonth)) - 1))));

                            MonthlyPaymentLabel.Text = $"Ежемесячный платеж: {Math.Round(EveryMonthPay, 2)}";
                            TotalLabel.Text = $"Общая сумма: {Math.Round(EveryMonthPay * (Convert.ToDouble(EntryTermMonth)), 2)}";
                            OverpaymentLabel.Text = $"Переплата: {Math.Round(Math.Round((Convert.ToDouble(EntryTermMonth) - Convert.ToDouble(EntryLoanAmount))), 2)}";
                        }
                        break;
                    default:
                        {
                            MonthlyPaymentLabel.Text = "Ежемесячный платеж:...";
                            TotalLabel.Text = "Общая сумма:...";
                            OverpaymentLabel.Text = "Переплата:...";
                        }
                        break;
                }
            }
            else
            {
                MonthlyPaymentLabel.Text = "Ежемесячный платеж: N/A";
                TotalLabel.Text = "Общая сумма: N/A";
                OverpaymentLabel.Text = "Переплата: N/A";
            }
        }
    }
}