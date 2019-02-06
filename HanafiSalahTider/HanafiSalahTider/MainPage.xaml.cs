using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HanafiSalahTider
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
           ReadFromTxtFile vm = new ReadFromTxtFile();
            this.BindingContext = vm;


            label1.Text = ReadFromTxtFile.Instanstider.Fajr;
            label2.Text = ReadFromTxtFile.Instanstider.Shuruk;
            label3.Text = ReadFromTxtFile.Instanstider.Dhuhr;
            label4.Text = ReadFromTxtFile.Instanstider.Asr;
            label5.Text = ReadFromTxtFile.Instanstider.Maghrib;
            label6.Text = ReadFromTxtFile.Instanstider.Isha;

        }
    }
}
