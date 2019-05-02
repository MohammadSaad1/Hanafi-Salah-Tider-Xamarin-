using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HanafiSalahTider
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        public MainPage()
        {
            InitializeComponent();

            ReadFromTxtFile vm = new ReadFromTxtFile();
           DateTime dt = DateTime.Now;
             
            this.BindingContext = vm;

           

            bedetidPlacering.Items.Add("Malmö");
            bedetidPlacering.Items.Add("København");
            bedetidPlacering.Items.Add("Stockholm");

            bedetidPlacering.SelectedIndex = 1;
            bedetidPlacering.Focus();
            loadPrayerTimes();
           CurrentSalahTime();



        }

        private void BedetidPlacering_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReadFromTxtFile vm = new ReadFromTxtFile();

            if(bedetidPlacering.SelectedIndex == 1)
            {
                vm.getText(DateTime.Now, DateTime.Now, "HanafiSalahTider.salahtider.txt", "HanafiSalahTider.ishatid.txt");
                loadPrayerTimes();
            }

            else if(bedetidPlacering.SelectedIndex == 0)
                    {
                vm.getText(DateTime.Now, DateTime.Now, "HanafiSalahTider.salahtidermalmo2019.txt", "HanafiSalahTider.salahtidermalmoIsha.txt");
                loadPrayerTimes();
            }

            else
            {
                vm.getText(DateTime.Now, DateTime.Now, "HanafiSalahTider.stockholmtid.txt", "HanafiSalahTider.stockholmtidIsha.txt");
                loadPrayerTimes();
            }

        }

        private void CurrentSalahTime()
        {
            var rtxt = ReadFromTxtFile.Instanstider;

            TimeSpan Start = new TimeSpan(rtxt.Dhuhr.Hour, rtxt.Dhuhr.Minute, 0);
            TimeSpan End = new TimeSpan(rtxt.Asr.Hour, rtxt.Asr.Minute, 0);
            TimeSpan Current = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);

            if (Current >= Start && Current < End)
            {
                dhuhrlayout.BackgroundColor = Color.LawnGreen;
            }

            Start = new TimeSpan(rtxt.Asr.Hour, rtxt.Asr.Minute, 0);
            End = new TimeSpan(rtxt.Maghrib.Hour, rtxt.Maghrib.Minute, 0);

            if (Current >= Start && Current < End)
            {
                asrlayout.BackgroundColor = Color.LawnGreen;
            }

            Start = new TimeSpan(rtxt.Maghrib.Hour, rtxt.Maghrib.Minute, 0);
            End = new TimeSpan(rtxt.Isha.Hour, rtxt.Isha.Minute, 0);

            if (Current >= Start && Current < End)
            {
                maghriblayout.BackgroundColor = Color.LawnGreen;
            }

            Start = new TimeSpan(rtxt.Isha.Hour, rtxt.Isha.Minute, 0);
            End = new TimeSpan(23, 59, 0);

            if (Current >= Start && Current <= End)
            {
                ishalayout.BackgroundColor = Color.LawnGreen;
            }

            Start = new TimeSpan(0, 0, 0);

            End = new TimeSpan(rtxt.Fajr.Hour, rtxt.Fajr.Minute, 0);

            if (Current >= Start && Current < End)
            {
                ishalayout.BackgroundColor = Color.LawnGreen;
            }

            Start = new TimeSpan(rtxt.Fajr.Hour, rtxt.Fajr.Minute, 0);
            End = new TimeSpan(rtxt.Shuruk.Hour, rtxt.Shuruk.Minute, 0);

            if (Current >= Start && Current < End)
            {
                fajrlayout.BackgroundColor = Color.LawnGreen;
            }
            var shuruk = rtxt.Shuruk.AddMinutes(25);
            TimeSpan ShurukEnd = new TimeSpan(shuruk.Hour, shuruk.Minute, 0);

            Start = new TimeSpan(rtxt.Shuruk.Hour, rtxt.Shuruk.Minute, 0);
            End = new TimeSpan(ShurukEnd.Hours, ShurukEnd.Minutes, 0);





            if (Current >= Start && Current < ShurukEnd)
            {
                shuruklayout.BackgroundColor = Color.Red;

            }

            var zawal = rtxt.Dhuhr.AddMinutes(-10);
            End = new TimeSpan(rtxt.Dhuhr.Hour, rtxt.Dhuhr.Minute, 0);

            TimeSpan Zawal = new TimeSpan(zawal.Hour, zawal.Minute, 0);
          
                if (Current >= Zawal && Current < End)
                {
                    zawallayout.BackgroundColor = Color.Red;

                }

            
        }

        private void loadPrayerTimes()
        {
            label0.Text = ReadFromTxtFile.Instanstider.Imsak.ToString("H:mm");
            label1.Text = ReadFromTxtFile.Instanstider.Fajr.ToString("H:mm");
            label2.Text = ReadFromTxtFile.Instanstider.Shuruk.ToString("H:mm");
            label3.Text = ReadFromTxtFile.Instanstider.Dhuhr.ToString("H:mm");
            label4.Text = ReadFromTxtFile.Instanstider.Asr.ToString("H:mm");
            label5.Text = ReadFromTxtFile.Instanstider.Maghrib.ToString("H:mm");
            label6.Text = ReadFromTxtFile.Instanstider.Isha.ToString("H:mm");
        }
    }
}
